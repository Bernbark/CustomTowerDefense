using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save 
{
    private string username;
    private string savestring;

    public Save(string user, string save)
    {
        this.username = user;
        this.savestring = save;
        Debug.Log(username);
        Debug.Log(savestring);
    }
}
