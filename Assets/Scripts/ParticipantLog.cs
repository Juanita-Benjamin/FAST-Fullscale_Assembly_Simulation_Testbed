using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ParticipantLog : MonoBehaviour
{
    public TMP_InputField PID;
    public TMP_Dropdown cohort_dropdown, gender_dropdown, race_dropdown, trial_dropdown;
    public Toggle embodiment, vhInstructor;
    public bool isIntructor, isEmbodied;
    public GameObject virtual_instructor, instructionSet;
    public GameObject[] avatars;
    public Transform spawnPoint;

    [Header("Avatar Prefabs")]

    private Dictionary<string, GameObject> avatarPrefabs;

    //don't use readonly key word
    string log_path = "Participant_Logs.csv";

    // Start is called before the first frame update
    void Start()
    {
        avatarPrefabs = new Dictionary<string, GameObject>()
        {
            {"Asian_F", avatars[0]},
            {"Asian_M", avatars[1] },
            {"Black_F", avatars[2] },
            {"Black_M", avatars[3] },
            {"Hispanic_F", avatars[4] },
            {"Hispanic_M",avatars[5] },
            {"White_F", avatars[6]},
            {"White_M", avatars[7]}
        };

    }

    // Update is called once per frame
    void Update()
    {
       PLog();
   
    }

    //handles the between subjects design
    public void PLog()
    {
        switch (cohort_dropdown.value)
        {
            case 1:
                isIntructor = false;
                vhInstructor.isOn = false;
                virtual_instructor.SetActive(false);
                instructionSet.SetActive(true);
                break;

            case 2:
                isIntructor = false;
                vhInstructor.isOn = true;
                virtual_instructor.SetActive(true);
                instructionSet.SetActive(false);
                break;
            case 3:
                isIntructor = true;
                vhInstructor.isOn = true;
                virtual_instructor.SetActive(true);
                instructionSet.SetActive(true);
                break;
        }

        
    }
    
    public void InstantiateAvatar()
    {
        
        
        //handles the within subjects design
        if (trial_dropdown.value == 1)
        {
            embodiment.isOn = false;
            
        }
        else
        {
            embodiment.isOn = true;
            

            //Getting the selected race and gender from dropdowns
            string selectedRace = race_dropdown.options[race_dropdown.value].text;
            string selectedGender = gender_dropdown.options[gender_dropdown.value].text;

            //create a key based 
            string key = $"{selectedRace}_{selectedGender}";
          


            if (avatarPrefabs.ContainsKey(key))
            {
                Debug.Log("Here");
                Debug.Log("Prefab shown");
                foreach (Transform child in spawnPoint)
                {
                    Destroy(child.gameObject);
                }

                GameObject prefab = Instantiate(avatarPrefabs[key]);
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            }
            else
            {
                Debug.LogWarning("No avatar found");
            }
        }
    }


    //this is to log in the spreadsheet what each person did
    public void ParticipantData()
    {
        string dateTime = DateTime.Now.ToString("MM-dd-yy-HH-mm");
        string ParticipantID = PID.GetComponent<TMP_InputField>().text;
        bool embody = embodiment.GetComponent<TMP_Text>();
        bool vh_instructor = vhInstructor.GetComponent<TMP_Text>(); 
        string avatar_gender = gender_dropdown.options[gender_dropdown.value].text;
        string avatar_race = race_dropdown.options[race_dropdown.value].text;
        string trial = trial_dropdown.options[trial_dropdown.value].text;

        if (!File.Exists(log_path))
        {
            using StreamWriter writer = File.CreateText(log_path);
            writer.WriteLine("ParticipantID, Gender, Race, isEmbodied, VH_Instructor, Trial #, date-time");

            writer.WriteLine($"{ParticipantID}, {avatar_gender}, {avatar_race},{embody},{vh_instructor}, {trial}, {dateTime}");
            writer.Close();
        }

        else 
        { 
            using StreamWriter writer = File.AppendText(log_path);
            writer.WriteLine($"{ParticipantID}, {avatar_gender}, {avatar_race},{embody},{vh_instructor}, {trial}, {dateTime}");
            writer.Close();
        }

    }
}
