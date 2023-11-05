﻿// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace Scripts.Character.Player.Handlers
// {
//     public class CursorController : MonoBehaviour
//     {
//         [SerializeField] private Camera _camera;
//         [FormerlySerializedAs("_character")] [SerializeField] private Classes.Person person;
//         [SerializeField] private ItemHandler _itemHandler;
//         [SerializeField] private float _requriedDistance;
//         
//         private void Update()
//         {
//             SetPosition(GetMousePosition());
//         }
//
//         #region Position
//
//         private void SetPosition(Vector2 mousePos) => transform.position = new(mousePos.x, mousePos.y);
//
//         private Vector3 GetMousePosition() => _camera.ScreenToWorldPoint(Input.mousePosition);
//
//         #endregion
//
//         #region Trigger
//
//         private void OnTriggerStay2D(Collider2D obj)
//         {
//             if (obj.gameObject.TryGetComponent(out TransportableItem item) && DistanceIsReached(obj.transform))
//                 _itemHandler.SelectItem(item);
//         }
//         
//         private void OnTriggerExit2D(Collider2D obj)
//         {
//             if (obj.gameObject.TryGetComponent(out TransportableItem item))
//                 _itemHandler.DeselectItem(item);
//         }
//
//         private bool DistanceIsReached(Transform obj)
//         {
//             float currentDistance = Vector2.Distance(person.transform.position, obj.position);
//
//             if (currentDistance <= _requriedDistance)
//                 return true;
//
//             return false;
//         }
//
//         #endregion
//     }
// }