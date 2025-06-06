﻿using System.Collections.Generic;
using System.IO;
using NeuroDerby.FileOperations;
using NeuroDerby.RatingSystem.Glicko;
using UnityEngine;

namespace NeuroDerby.Players
{
    public class PlayersDtoLoader : IPlayersDtoLoader
    {
        private readonly PathConfig _pathConfig;

        public PlayersDtoLoader(PathConfig pathConfig)
        {
            _pathConfig = pathConfig;
        }
        
        public List<PlayerDto> Load()
        {
            return FileLoader.Load<List<PlayerDto>>(Path.Combine(Application.persistentDataPath, _pathConfig.PersistentPlayerDataPathPostfix));
        }
    }
}