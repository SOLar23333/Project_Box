using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ThemeInformationSet : MonoBehaviour
{
    public int theme;
    public string[] passedLevels;
    // Start is called before the first frame update
    private void Awake()
    {
        string[] passed = {"ori"};
        PassedlevelSaving passedlevelSaving = new PassedlevelSaving();
        try 
        {
            passedlevelSaving = JsonUtility.FromJson<PassedlevelSaving>( File.ReadAllText( Application.persistentDataPath + "/passed_level_save.json" ) );
            passed = passedlevelSaving.passed;
        }
        catch
        {
            //passedlevelSaving = passedLevels;
        }


        if (passed == null)
        {
            passed = new string[] {"ori"};
        }  

        passedLevels = passed;
    } 
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
