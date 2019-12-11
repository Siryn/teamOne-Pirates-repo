using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    //private Image content;
    [SerializeField]
    private Text _healthValueText, _currencyText;

    public float myMaxValue { get; set; }

    private float _currentValue;
    public float MyCurrentValue
    {
        get
        {
            return _currentValue;
        }

        set
        {
            if (value > myMaxValue)
            {
                _currentValue = myMaxValue;
            }
            else if (value < 0)
            {
                MyCurrentValue = 0;
            }
            else
            {
                _currentValue = value;
            }

            //currentFill = _currentValue / myMaxValue;

            if (_healthValueText != null)
            {
                _healthValueText.text = "HP: " + _currentValue + " / " + myMaxValue;
            }
            if (_currencyText != null)
            {
                _currencyText.text = "$" + _currentValue;
            }
        }
    }

    private float _currentFill;
    /*public float currentFill
    {
        get
        {
            return _currentFill;
        }

        set
        {
            _currentFill = value;
        }
    }*/


    void Start()
    {

	}
	
	void Update()
    {
        //content.fillAmount = _currentFill;
        /*if (_statValueText != null)
        {
            _statValueText.text = "Health: " + _currentValue + " / " + myMaxValue;
        }*/
    }

    public void Initialize(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
