using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_life : MonoBehaviour
{
    private int maxHearts = 3; // �ő�n�[�g��
    public Image heartImagePrefab; // �n�[�g�̉摜�v���n�u
    public Transform heartContainer; // �n�[�g�̐e�I�u�W�F�N�g

    //�@���ꂽ���ɕ\������UI
    [SerializeField]
    private GameObject gameoverUI;

    private Image[] hearts; // �n�[�g�̔z��
    private int currentHearts; // ���݂̃n�[�g��

    player_life lifescript;

    private void Start()
    {
        lifescript = GetComponent<player_life>();
        maxHearts = lifescript.life;
        hearts = new Image[maxHearts]; // �n�[�g�̔z���������
        currentHearts = maxHearts; // ���݂̃n�[�g�����ő�n�[�g���ɐݒ�

        // �n�[�g�̐������n�[�g�摜�𐶐����Ĕz��Ɋi�[
        for (int i = 0; i < maxHearts; i++)
        {
            Image heart = Instantiate(heartImagePrefab, heartContainer);
            hearts[i] = heart;
        }
    }

    void Update()
    {
        if(lifescript.life < maxHearts)
        {
            DecreaseLife();
            maxHearts = lifescript.life;
        }

        if (lifescript.life <= 0 && !gameoverUI.activeSelf)
        {
            //�@�Q�[���I�[�o�[UI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
            gameoverUI.SetActive(!gameoverUI.activeSelf);
        }
    }

    // ���C�t�����������ɌĂяo���֐�
    public void DecreaseLife()
    {
        if (currentHearts > 0)
        {
            currentHearts--; // ���݂̃n�[�g�������炷
            hearts[currentHearts].enabled = false; // �Ō�̃n�[�g���\���ɂ���
        }
    }

    // ���C�t�����������ɌĂяo���֐�
    public void IncreaseLife()
    {
        if (currentHearts < maxHearts)
        {
            hearts[currentHearts].enabled = true; // ���̃n�[�g��\������
            currentHearts++; // ���݂̃n�[�g���𑝂₷
        }
    }
}
