using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NeuroDerby.Core;
using UnityEngine;

namespace NeuroDerby.Game
{
    public class PlayerActionsLogging : MonoBehaviour
    {
        [SerializeField] private LoggingSystem logger;
        [SerializeField] private List<PlayerHierarchy> playerHierarchies;

        private Coroutine logCoroutine;
        private Coroutine waitHeadersAndWriteCoroutine;
        private List<MoveEventData> storedEventData = new List<MoveEventData>();

        private bool areHeadersAdded = false;

        private void Awake()
        {
            if (logger == null)
                Debug.Log("[Logging] Unable to set reference to Logging System.");
        }

        private void Start()
        {
            foreach (var playerHierarchy in playerHierarchies)
            {
                playerHierarchy.InputController.MoveEvent.PlayerActionsLogging = this;
            }
        }

        public void OnMoveEvent(MoveEventData moveEventData)
        {
            storedEventData.Add(moveEventData);

            if (!areHeadersAdded && waitHeadersAndWriteCoroutine == null)
                waitHeadersAndWriteCoroutine = StartCoroutine(WaitHeadersAndWrite());
            else if (areHeadersAdded)
                WriteLogs();
        }

        private IEnumerator WaitHeadersAndWrite()
        {
            yield return new WaitUntil(() => areHeadersAdded);
            WriteLogs();
        }

        private void WriteLogs()
        {
            if (storedEventData.Count >= playerHierarchies.Count)
            {
                var logs = new List<string>();
                var collectedPlayerDataCount = 0;
                foreach (var playerData in storedEventData.OrderBy(data => data.PlayerNum))
                {
                    var playerHealth = playerHierarchies[playerData.PlayerNum].Health;
                    logs.AddRange(new[]
                    {
                    $"{playerData.X:F}", $"{playerData.Y:F}", playerHealth.healthText.text,
                    $"{playerData.HDirection}", $"{playerData.VDirection}"
                });
                    collectedPlayerDataCount++;
                    if (collectedPlayerDataCount == playerHierarchies.Count)
                        Log(logs.ToArray());
                }

                storedEventData.Clear();
            }
        }

        public void StartLog()
        {
            if (logger.activeLogging)
                logCoroutine = StartCoroutine(LogActions());
        }

        public void StopLog()
        {
            if (logCoroutine != null)
            {
                StopCoroutine(logCoroutine);
                logCoroutine = null;
            }
        }

        private IEnumerator LogActions()
        {
            yield return new WaitUntilOrSeconds(() => logger.IsFileCreated, 5f);

            if (!logger.IsFileCreated)
            {
                Debug.LogWarning("Log file was not created");
                yield break;
            }

            var logs = new List<string>();
            var playerNumForLog = 1;
            foreach (var _ in playerHierarchies)
            {
                logs.AddRange(new[] { $"x{playerNumForLog}", $"y{playerNumForLog}", $"hp{playerNumForLog}", $"hdirection{playerNumForLog}", $"vdirection{playerNumForLog}" });
                playerNumForLog++;
            }

            Log(logs.ToArray());

            areHeadersAdded = true;
        }

        private void Log(params string[] args)
        {
            logger.writeMessageWithTimestampToLog(string.Join(";", args));
        }
    }
}