using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerMagicSystem : MonoBehaviour
    {
        [SerializeField] private Transform castPoint;

        [SerializeField] private InputAction spellCastQ;

        private bool _isCastingMagic;
        
        private void OnEnable()
        {
            spellCastQ.Enable();
            spellCastQ.performed += SpellCastQ;
        }

        private void OnDisable()
        {
            spellCastQ.performed += SpellCastQ;
            spellCastQ.Disable();
        }
        
        private void SpellCastQ(InputAction.CallbackContext context)
        {
            if (_isCastingMagic) return;
            _isCastingMagic = true;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point;
                transform.LookAt(targetPosition);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                StartCoroutine(CastBolt(targetPosition));
            }
        }
        
        private IEnumerator CastBolt(Vector3 targetPosition)
        {
            yield return new WaitForSeconds(0.5f);
            //GameObject bolt = ObjectPoolManager.SpawnObject()
            _isCastingMagic = false;
            Debug.Log("Cast Bolt");
            //TODO: Mouse tıklamasına kadar beklet sonra at, Animasyon başlat, koşu animasyonunu kesmeli, SO systemi oluştur, Enemy gelince kapat ve enemyi geriye fırlat, boşluğa giderse kapat bir süre sonra
        }
    }
    
}