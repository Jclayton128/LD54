using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sr = null;


    //state
    [SerializeField] int _currentHealth = 1;
    public SiteController.GeneralSites GeneralSiteType = SiteController.GeneralSites.Empty;
    StructureHandler _currentStructure;
    public StructureHandler CurrentStructure=> _currentStructure;

    private void Start()
    {
        BuildInitialStructure();
    }

    private void BuildInitialStructure()
    {
        switch (GeneralSiteType)
        {
            case SiteController.GeneralSites.Empty:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.EmptyLot).GetComponent<StructureHandler>(),
                    transform);
                break;

            case SiteController.GeneralSites.Cratered:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.Crater).GetComponent<StructureHandler>(),
                    transform);
                break;

            case SiteController.GeneralSites.House:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.House_Basic).GetComponent<StructureHandler>(),
                    transform);
                break;

            case SiteController.GeneralSites.Farm:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.Farm_Basic).GetComponent<StructureHandler>(),
                    transform);
                break;

            case SiteController.GeneralSites.Mine:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.Mine_Basic).GetComponent<StructureHandler>(),
                    transform);
                break;

            case SiteController.GeneralSites.Lab:
                _currentStructure =
                    Instantiate(StructureLibrary.Instance.
                    GetPrefabFromMenu(StructureLibrary.Structures.Lab_Basic).GetComponent<StructureHandler>(),
                    transform);
                break;
        }
    }


    public void Highlight()
    {
        _currentStructure.SpriteRenderer.color = ColorLibrary.Instance.HighlightedStructure;
    }

    public void Lowlight()
    {
        _currentStructure.SpriteRenderer.color = ColorLibrary.Instance.LowlightedStructure;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AsteroidHandler ah;
        if (collision.TryGetComponent<AsteroidHandler>(out ah))
        {
            ah.HandleStructureImpact();
        }
    }
}
