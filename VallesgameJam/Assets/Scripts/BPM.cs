using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour {

    public AudioClip mSonidoBG;
    public AudioClip mSonidoFX1;
    public AudioClip mSonidoFX2;

    private AudioSource mEmisor;

    // Temporizadores de BPM
    private float mSegundosBPM4;
    private float mSegundosBPM2;
    private float mSegundosBPM1;
    private float mSegundosBPM05;

    // Control de captura de acciones
    private bool mFire1Press;
    private bool mFire2Press;

    // Control de eventos
    private bool mMustPlayBG;
    private bool mMustPlayFX1;
    private bool mMustPlayFX2;


    private const float mBPM4 = 2.4f;
    private const float mBPM2 = 1.2f;
    private const float mBPM1 = 0.6f;
    private const float mBPM05 = 0.3f;
    private const float mMargenSegundos1 = mBPM1 / 10.0f;
    private const float mMargenSegundos05 = mBPM05 / 10.0f;

    // Solo para pintado
    public GameObject mGUIBPM4;
    public GameObject mGUIBPM2;
    public GameObject mGUIBPM1;
    public GameObject mGUIBPM05;

    private SpriteRenderer mSpriteBPM4;
    private SpriteRenderer mSpriteBPM2;
    private SpriteRenderer mSpriteBPM1;
    private SpriteRenderer mSpriteBPM05;

    Vector3 position1Init;
    Vector3 position2Init;
    float position1OffsetY;
    float position2OffsetY;


    void Awake ()
    {
        mEmisor = GetComponent<AudioSource>();
        mSpriteBPM4 = mGUIBPM4.GetComponent<SpriteRenderer>();
        mSpriteBPM2 = mGUIBPM2.GetComponent<SpriteRenderer>();
        mSpriteBPM1 = mGUIBPM1.GetComponent<SpriteRenderer>();
        mSpriteBPM05 = mGUIBPM05.GetComponent<SpriteRenderer>();
        position1Init = mGUIBPM1.GetComponent<Transform>().position;
        position2Init = mGUIBPM05.GetComponent<Transform>().position;
    }

	// Use this for initialization
	void Start () {
        mSegundosBPM4 = 0;
        mSegundosBPM2 = 0;
        mSegundosBPM1 = 0;
        mSegundosBPM05 = 0;
        mMustPlayBG = false;
        mMustPlayFX1 = false;
        mMustPlayFX2 = false;

        position1OffsetY = 0;
        position2OffsetY = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float tempDelta = Time.deltaTime;

        // Captura de botones
        if (Input.GetButtonDown("Fire1"))
        {
            mFire1Press = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            mFire2Press = true;
        }

        // Margenes de permitir activar evento
        if (mSegundosBPM1 > mMargenSegundos1)
        {
            mMustPlayFX1 = false;
        }
        if (mSegundosBPM05 > mMargenSegundos05)
        {
            mMustPlayFX2 = false;
        }

        // Controles de temporizadores
        if (mSegundosBPM4 > mBPM4)
        {
            // Aparte de los que le corresponde al temporizador, 
            //   hay que reiniciar los que dependen de tiempos menores porque sino se desfasan
            mSegundosBPM4 = 0;
            mSegundosBPM2 = 0;
            mSegundosBPM1 = 0;
            mSegundosBPM05 = 0;
            mMustPlayBG = true;
            mMustPlayFX1 = true;
            mMustPlayFX2 = true;
        }
        if (mSegundosBPM2 > mBPM2)
        {
            // Aparte de los que le corresponde al temporizador, 
            //   hay que reiniciar los que dependen de tiempos menores porque sino se desfasan
            mSegundosBPM2 = 0;
            mSegundosBPM1 = 0;
            mSegundosBPM05 = 0;
            mMustPlayFX1 = true;
            mMustPlayFX2 = true;
        }
        if (mSegundosBPM1 > mBPM1)
        {
            // Aparte de los que le corresponde al temporizador, 
            //   hay que reiniciar los que dependen de tiempos menores porque sino se desfasan
            mSegundosBPM1 = 0;
            mSegundosBPM05 = 0;
            mMustPlayFX1 = true;
            mMustPlayFX2 = true;
        }
        if (mSegundosBPM05 > mBPM05)
        {
            // Aparte de los que le corresponde al temporizador, 
            //   hay que reiniciar los que dependen de tiempos menores porque sino se desfasan
            mSegundosBPM05 = 0;
            mMustPlayFX2 = true;
        }
        mSegundosBPM4 += tempDelta;
        mSegundosBPM2 += tempDelta;
        mSegundosBPM1 += tempDelta;
        mSegundosBPM05 += tempDelta;

        // Activación de enventos
        if (mMustPlayBG)
        {
            mEmisor.PlayOneShot(mSonidoBG, 1.0f);
            mMustPlayBG = false;
        }
        if ((mMustPlayFX1) && (mFire1Press))
        {
            mEmisor.PlayOneShot(mSonidoFX1, 1.0f);
            mFire1Press = false;
            mMustPlayFX1 = false;
            position1OffsetY = 3.0f;
        }
        if ((mMustPlayFX2) && (mFire2Press))
        {
            mEmisor.PlayOneShot(mSonidoFX2, 1.0f);
            mFire2Press = false;
            mMustPlayFX2 = false;
            position2OffsetY = 3.0f;
        }
        // Muestras visuales de temporizadores
        mSpriteBPM4.color = new Color(1.0f - mSegundosBPM4 / mBPM4, 0.0f, 0.0f);
        mSpriteBPM2.color = new Color(0.0f, 1.0f - mSegundosBPM2 / mBPM2, 0.0f);
        mSpriteBPM1.color = new Color(0.0f, 0.0f, 1.0f - mSegundosBPM1 / mBPM1);
        mSpriteBPM05.color = new Color(0.0f, 1.0f - mSegundosBPM05 / mBPM05, 1.0f - mSegundosBPM05 / mBPM05);

        Vector3 tempPos = position1Init + new Vector3(0.0f, position1OffsetY, 0.0f);
        mGUIBPM1.GetComponent<Transform>().position = tempPos;
        tempPos = position2Init + new Vector3(0.0f, position2OffsetY, 0.0f);
        mGUIBPM05.GetComponent<Transform>().position = tempPos;
        position1OffsetY -= 0.1f;
        position2OffsetY -= 0.2f;
        if (position1OffsetY < 0.0f)
        {
            position1OffsetY = 0.0f;
        }
        if (position2OffsetY < 0.0f)
        {
            position2OffsetY = 0.0f;
        }
    }
}
