using System.Collections.Generic;
using System.Linq;
using Kulikova.InventoryItemsAbilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulikova
{
    public class CraftService : MonoBehaviour
    {
        public Transform UIItemsRoot;
        public CraftSettings CraftSettings;

        public Button craftButton;

        private List<ICraftable> _craftablesItems = new List<ICraftable>();
        private List<GameObject> _selected = new List<GameObject>();

        private CharacterData _characterData;
        private bool _onClick;

        [Inject]
        private void Construct(CharacterData characterData)
        {
            _characterData = characterData;
            craftButton.onClick.AddListener(CraftClick);
        }

        private void CraftClick()
        {
            _onClick = !_onClick;

            if (_onClick)
            {
                craftButton.image.color = new Color(0.5f, 1f, 0.5f, 0.7f);
                EnterCraftMode();
            }
            else
            {
                craftButton.image.color = new Color(1f, 0.5f, 0.5f, 0.7f);
                _selected.Clear();
                _craftablesItems.Clear();
            }
        }

        private void EnterCraftMode()
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

                    var craftable = newItem.GetComponent<IAbilityTarget>();

                    if (craftable.Targets.Count == 0)
                        craftable.Targets.Add(_characterData.gameObject);
                }
            }
        }
    }
}