using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBarRefresh : MonoBehaviour

{
    [SerializeField]
    private TMPro.TextMeshProUGUI txt;
    [SerializeField]
    private Slider slider;
    private int Hp;

    private void Start()
    {
        Hp = 100;
        UpdateHP(Hp);
    }
    public void UpdateHP(int HP)
    {
        Hp = HP;
        txt.text = HP.ToString() + ("/100");
        slider.value = HP;


    }
}
