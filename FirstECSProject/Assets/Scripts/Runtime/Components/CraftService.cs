using System.Collections.Generic;
using System.Linq;
using Kulikova.InventoryItemsAbilities;
using UnityEngine;
using UnityEngine.UI;

namespace Kulikova
{
    public class CraftService : MonoBehaviour
    {
        public Transform UIItemsRoot;
        public CraftSettings CraftSettings;

        private List<ICraftable> _craftablesItems = new List<ICraftable>();
        private List<GameObject> _selected = new List<GameObject>();

        public void EnterCraftMode()
        {
            _selected.Clear();

            _craftablesItems = GetComponentsInChildren<ICraftable>().ToList();

            foreach (var item in _craftablesItems)
            {
                var button = ((MonoBehaviour)item)?.gameObject.GetComponent<Button>();
                button.onClick.AddListener(() => Select(button.gameObject));
            }
        }

        private void Select(GameObject obj)
        {
            if (_selected.Contains(obj))
            {
                _selected.Remove(obj);
                obj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                _selected.Add(obj);
                obj.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f, 0.7f);
            }

            CheckCombo();
        }

        private void CheckCombo()
        {
            List<string> selectedNames = new List<string>();
            foreach (var item in _selected)
            {
                var name = item.GetComponent<ICraftable>().Name;
                selectedNames.Add(name);
            }

            foreach (var combination in CraftSettings.combinations)
            {
                if (combination.sources.SequenceEqual(selectedNames))
                {
                    foreach (var victim in _selected)
                        Destroy(victim);
                    var newItem = Instantiate(combination.result, UIItemsRoot);
                }
            }
        }
    }
}