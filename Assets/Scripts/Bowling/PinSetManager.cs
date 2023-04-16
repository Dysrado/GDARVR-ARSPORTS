using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pins;
    [SerializeField] private List<KnockedPinManager> knockedPinManagers;
    public int knockedPins = 0;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private BowlingScoreManager bowlingScoreManager;
    [SerializeField] private BowlingUIBehaviour bowlingUIBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = Object.FindObjectOfType<UIManager>();
        uIManager.pinSetManager = this;
        bowlingScoreManager = Object.FindObjectOfType<BowlingScoreManager>();
        bowlingScoreManager.pinSetManager = this;
        bowlingUIBehaviour = Object.FindObjectOfType<BowlingUIBehaviour>();
        bowlingUIBehaviour.pinSetManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Reset Pins
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(this.gameObject);
        }
    }


    public int CheckKnockedOverPins()
    {
        for (int i = 0; i < knockedPinManagers.Count; i++)
        {
            if(knockedPinManagers[i].CheckIfPinKnocked())
            {
                knockedPins++;
                knockedPinManagers[i].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        return knockedPins;
    }

    public void ResetSparePinsAttempt()
    {
        for (int i = 0; i < knockedPinManagers.Count; i++)
        {
            if (knockedPinManagers[i].CheckIfPinKnocked())
            {
                knockedPinManagers[i].isKnockedOver = false;
                knockedPinManagers[i].hit1stAttempt = true;
            }
        }
    }

    public void ResetKnockedPinsState()
    {
        for (int i = 0; i < knockedPinManagers.Count; i++)
        {
            knockedPinManagers[i].isKnockedOver = false;
        }
    }

    public void DestroyPinSet()
    {
        Destroy(this.gameObject);
    }

}
