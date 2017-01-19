using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseManagerScript : MonoBehaviour {
    //private connection strings to databases
    private string characterDBConnString;
	// Use this for initialization
	void Awake () {
        //format connection strings
        characterDBConnString = "URI=file:" + Application.dataPath + "/Databases/CharacterDB.sqlite";

        LoadCharacters();
	}

    //Populates a public list with Characters from the database
    public void LoadCharacters()
    {
        using (IDbConnection dbConnection = new SqliteConnection(characterDBConnString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Character";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    //reads each record then assigns the record to the CharacterList
                    while (reader.Read())
                    {
                        CharacterManagerScript.FullCharacterList.Add(new CharacterScript(reader.GetInt32(0), reader.GetString(1)));
                        Debug.Log(String.Format("ID: {0} Name: {1}", reader.GetInt32(0), reader.GetString(1)));
                    }
                    //close connections
                    reader.Close();
                    dbConnection.Close();
                }
            }
        }
    }
    //Grabs data from the player list and updates the character database
    public void SaveCharacters()
    {
        using (IDbConnection dbConnection = new SqliteConnection(characterDBConnString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                foreach (var character in CharacterManagerScript.FullCharacterList)
                {
                    string sqlQuery = String.Format("UPDATE Character " +
                                                    "SET Name = '{0}' " +
                                                    "WHERE ID = {1} ", character.name, character.ID);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                }
                dbConnection.Close();
            }
        }
    }
}
