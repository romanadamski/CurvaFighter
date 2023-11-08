using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField]
    private List<BaseMenu> menuPrefabs;

    private List<BaseMenu> _menus = new List<BaseMenu>();

    [Inject]
    private DiContainer _container;
    [Inject]
    private Canvas _canvas;

    private void Awake()
    {
        CreateMenus();
        HideAllMenus();
    }

    private void CreateMenus()
    {
        foreach (var menu in menuPrefabs)
        {
            _menus.Add(_container.InstantiatePrefab(menu, _canvas.transform).GetComponent<BaseMenu>());
        }
    }

    private void HideAllMenus()
    {
        foreach (var menu in _menus)
        {
            menu.Hide();
        }
    }

    public bool TryGetMenuByType<T>(out T menu) where T : BaseMenu
    {
        menu = _menus.FirstOrDefault(x => x.GetType().Equals(typeof(T))) as T;
        if (menu)
        {
            return true;
        }
        return false;
    }
}
