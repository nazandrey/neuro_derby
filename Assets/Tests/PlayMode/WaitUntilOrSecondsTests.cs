using System.Collections;
using NeuroDerby.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [Explicit("Специально используется задержка в тестах")]
    public class WaitUntilOrSecondsTests
    {
        const int Timeout = 100;
        const int Delta = 50;
        const int MsInSec = 1000;
        float waitingSeconds = (float)(Timeout - Delta) / MsInSec;

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator WaitUntilOrSeconds_AlwaysFalseCondition()
        {
            yield return new WaitUntilOrSeconds(() => false, waitingSeconds);
        }

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator WaitUntilOrSeconds_AlwaysTrueCondition()
        {
            yield return new WaitUntilOrSeconds(() => true, waitingSeconds);
        }

        [UnityTest]
        [Timeout(Timeout)]
        [Explicit("Ожидается, что тест провалится, чтобы не пугало, что здесь ошибка")]
        public IEnumerator WaitUntil_AlwaysFalseCondition()
        {
            yield return new WaitUntil(() => false);
        }

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator WaitUntil_AlwaysTrueCondition()
        {
            yield return new WaitUntil(() => true);
        }
    }
}
