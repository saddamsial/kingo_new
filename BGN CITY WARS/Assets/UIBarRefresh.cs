using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBarRefresh : MonoBehaviour

{
    [SerializeField]
    private TMPro.TextMeshProUGUI txt;
    [SerializeField]
    private Slider fill;
    private int Hp;

    private void Start()
    {
        UpdateHP(Hp);
    }
    public void UpdateHP(int HP)
    {
        Hp = HP;
        txt.text = HP.ToString() + ("/100");
        fill.value = HP;


    }
}
