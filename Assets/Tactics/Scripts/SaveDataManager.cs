using UnityEngine;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveDataManager : MonoBehaviour {

	public void Save()
    {
        //monta save data
        SaveData saveData = new SaveData();
        saveData.personagens = new List<StatusPersonagem>();
        Personagem[] personagensNaCena = FindObjectsOfType<Personagem>();
        foreach (Personagem p in personagensNaCena)
        {
            StatusPersonagem statusPersonagem = new StatusPersonagem();
            statusPersonagem.x = (int) p.transform.position.x;
            statusPersonagem.z = (int) p.transform.position.z;
            saveData.personagens.Add(statusPersonagem);
        }

        //salva o save data no arquivo saveDate.dat
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + "/saveGame.dat" , FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fileStream, saveData);
        fileStream.Close();
    }

    [Serializable]
    public class SaveData
    {
        public List<StatusPersonagem> personagens;
    }

    [Serializable]
    public class StatusPersonagem
    {
        public int x;
        public int z;
    }
}
