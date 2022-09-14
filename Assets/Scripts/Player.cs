using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapon;
    [SerializeField] private Transform _shootPoint;

    private int _currentWeaponNumber = 0;
    private Weapon _currentWeapon;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChaged;

    private void Start()
    {
        ChangeWeapon(_weapon[_currentWeaponNumber]);
        _currentWeapon = _weapon[0];
        _currentHealth = _health;
        _animator = GetComponent<Animator>();

    }

  
     private void Update()
     {
        if(Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Shoot");
            _currentWeapon.Shoot(_shootPoint);
         
        }

     }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Нанес урон");
            
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChaged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapon.Add(weapon);
        MoneyChaged?.Invoke(Money);
 
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapon.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;
        
        ChangeWeapon(_weapon[_currentWeaponNumber]);
       
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapon.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapon[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }



}
