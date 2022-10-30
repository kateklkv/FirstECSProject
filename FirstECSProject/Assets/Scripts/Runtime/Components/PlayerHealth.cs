using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityGoogleDrive.Data;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.IO;
using Kulikova.UI;
using Zenject;

namespace Kulikova
{
    public class PlayerHealth : MonoBehaviour, IConvertGameObjectToEntity
    {
        private Entity _entity;
        private EntityManager _dstManager;

        public bool IsApplyDamage { get; set; }

        [SerializeField] 
        private int _health = 1000;
        public int Health
        {
            get => _health;
            set
            {
                if (_health == value) return;
                
                _health = value;

                if (_canvasViewModel != null) _canvasViewModel.Health = string.Format("Health: {0}", _health.ToString());
                
                if (_health <= 0)
                {
                    _dstManager.DestroyEntity(_entity);
                    Destroy(gameObject);
                }
            }
        }

        private GameData _gameData = new GameData();

        private string _jsonFileId = "1A8eGrK3kFkLGxCXqQtYO1zdE1Q2M5ESw";

        private IConfiguration _configuration;

        private string _localFilePath = "D:/Kate//Project/GameData.json";
        private string _localJsonResult;

        private CanvasViewModel _canvasViewModel;

        [SerializeField]
        private ConfigurationSelector _playerData;

        [Inject]
        private void Construct(CanvasViewModel canvasViewModel) => _canvasViewModel = canvasViewModel;

        private void Awake()
        {          
            if (_playerData == null)
                _playerData = GetComponent<ConfigurationSelector>();

            //SerializeGoodleDriveData();
        }

        private void Start()
        {
            // Async operation
            //AsyncReadLocalFile();

            // Read from local file
            /*_gameData = JsonUtility.FromJson<GameData>(_localJsonResult);
            gameObject.name = _gameData.name;
            Health = _gameData.health;*/

            // Player data from configuration selector
            Health = _playerData.SelectedPlayerData.health;
        }

        private void OnDestroy()
        {
            /*_gameData.name = gameObject.name;
            _gameData.health = _health;
            DeserializeGoogleDriveData();*/
        }

        #region AsyncOperation

        /*private async void AsyncReadLocalFile()
        {
            await Task.Run(() =>
            {
                if (System.IO.File.Exists(_localFilePath))
                {
                    _localJsonResult = System.IO.File.ReadAllText(_localFilePath);
                }
            });
        }*/

        #endregion

        #region GoogleDriveDownload
        void SerializeGoodleDriveData()
        {
            GoogleDriveTools.FileList(OnDoneFileList);
        }

        private void OnDoneFileList(FileList fileList)
        {
            var files = fileList.Files;
            foreach (var file in files)
            {
                if (file.Id == _jsonFileId)
                {
                    GoogleDriveTools.Download(file.Id, OnDoneDownload);
                }
            }
        }

        private void OnDoneDownload(UnityGoogleDrive.Data.File file)
        {
            string jsonString = Encoding.UTF8.GetString(file.Content);

            _gameData = JsonUtility.FromJson<GameData>(jsonString);

            gameObject.name = _gameData.name;
            Health = _gameData.health;
        }

        private void DeserializeGoogleDriveData()
        {
            string jsonString = JsonUtility.ToJson(_gameData);
            GoogleDriveTools.Upload(_jsonFileId, jsonString, OnDoneUpload);
        }

        private void OnDoneUpload(UnityGoogleDrive.Data.File file) { }

        #endregion

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            _entity = entity;
            _dstManager = dstManager;

            dstManager.AddComponentData(entity, new HealthData
            {
                Health = Health
            });
        }
    }

    public struct HealthData : IComponentData
    {
        public int Health;
    }
}
