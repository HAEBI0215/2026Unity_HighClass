using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BulletLauncher launcherPrefab;
    BulletLauncher launcher;

    [SerializeField]
    Transform launcherLocator;
    // Start is called before the first frame update
    void Start()
    {
        launcher = Instantiate(launcherPrefab);
        launcher.transform.position = launcherLocator.position;

        MouseGameController mouseController = gameObject.AddComponent<MouseGameController>();
        mouseController.FireButtonPressed += launcher.OnFireButtonPressed;

        //KeyGameController keyController = gameObject.AddComponent<KeyGameController>();
        //keyController.FireButtonPressed += launcher.OnFireButtonPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
