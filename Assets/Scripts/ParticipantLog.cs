using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        //handles the within subjects design
        if (trial_dropdown.value == 1)
        {
            embodiment.isOn = false;
            Debug.Log("false");

        }
        else 
        { 
            embodiment.isOn = true;
            Debug.Log("true");
        }

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
                vhInstructor.isOn = false;
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


    //this is to log in the spreadsheet what each person did
    public void ParticipantData()
    {
        
    }
}
