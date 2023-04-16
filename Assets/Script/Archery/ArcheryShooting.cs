using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryShooting : MonoBehaviour
{
    [SerializeField] GameObject ArrowUI;
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject offset;
  
    [SerializeField] Transform parent;
    ArcheryScoring score;

    // Start is called before the first frame update
    void Start()
    {
        score = ArcheryScoring.Instance;
        //SampleTesting
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireArrow()
    {
        if (score.GetArrowsShot() < 5 && score.CanFire())
        {
            GameObject arrow = Instantiate(ArrowPrefab, offset.transform.position, offset.transform.rotation, parent);
            score.IncrementShot();
        }
    }
}
