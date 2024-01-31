using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class WeaponShoot : MonoBehaviour
{

    #region Variables
    private WeaponStatus weaponstatus;
    [SerializeField]
    private PlayerActionsVar Parentvariables;
    [Header("Weapon Specs")]
    public float FireRate;
    public float ReloadTime;
    public int BodyDamage;
    public int HeadDamage;
    public int TotalDamageDealt;
    public float WeaponRange;


    [Space(10)]
    [Header("Ammo Settings")]

    public int currentclip;
    public int MaxClip;
    public int totalammo;
    public int MaxAmmo;
    public int BulletsFired = 0;
    public bool Lowammo;
    public bool noammo;
    private float lastshot = 0f;
    [Space(10)]
    [Header("Firing Info")]
    public bool Canfire;
    private bool started;
    public bool Fired;
    public bool ButtonFired;
    public float modifiedFireRate;
    [Space(10)]
    [Header("Reload Info")]
    public bool Reloading;
    public bool ButtonReload;



    [HideInInspector]
    public bool bodyshotHit;
    [HideInInspector]
    public bool headshotHit;
    [Header("Weapon Settings")]
    [SerializeField]
    private LayerMask layermask;
    private Vector3 point;
    private Transform pos;
    private Transform Shootpoint;
    private RaycastHit hit;
    private RaycastHit hit2;

    [Header("Weapon Audio")]
    private AudioSource AS;
    [SerializeField]
    private AudioClip FireSFX;
    [SerializeField]
    private AudioClip BodyshotSFX;
    [SerializeField]
    private AudioClip HeadshotSFX;


    private Transform PlayerParent;
    // VFX SPAWN
    [Header("WeaponVFX")]
    public ParticleSystem BulletTrailVFX;
    public ParticleSystem BulletDropVFX;
    public GameObject BulletHoleVFX;
    private GameObject CameraMain;
    private GameObject ScopeUI;
    private Animator ScopeAnimator;

    //pun variables
    [Header("Debugs")]
    private PhotonView PV;
    private Collider collided;
    private GameObject KillFeed;
    private GameObject HeadShotKill;
    private int TargetHP;
    private int TargetShield;
    private PhotonView TPV;
    private bool hasExecutedKill = false;
    private float DamageDelay;
    private int LastDamageType;
    private GameObject HitReticleCrosshair;
    private TextMeshProUGUI AmmoMessage;

    #endregion Variables

    private void OnEnable()
    {
        #region CrosshairSetUp
        Transform DefaultReticle;
        DefaultReticle = GameObject.Find("DEFAULT RETICLE").transform;
        HitReticleCrosshair = DefaultReticle.transform.GetChild(1).gameObject;
        ScopeUI = DefaultReticle.transform.GetChild(3).gameObject;
        ScopeAnimator = ScopeUI.GetComponent<Animator>();
        #endregion
  
        AS = GetComponent<AudioSource>();

        #region find  and assign kill pop up feeds.
        KillFeed = GameObject.Find("KILL FEEDS").transform.GetChild(0).gameObject;
        HeadShotKill = GameObject.Find("KILL FEEDS").transform.GetChild(1).gameObject;

        #endregion
        Invoke("FindParent", .5f);
        PV = this.GetComponent<PhotonView>();
        AmmoRefresh();
        collided = hit.collider;
   
      
    

        // start reload after weapon pull//
        if (currentclip < 1 && weaponstatus.NoAmmo != true)
        {
            StartCoroutine(Reload());
        }
     
    }
    private void OnDisable()
    {
        Reloading = false;
        bodyshotHit = false;
        headshotHit = false;

    }
    void FindParent()
    {
        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        weaponstatus = PlayerParent.GetComponent<WeaponStatus>();
        Parentvariables = PlayerParent.GetComponent<PlayerActionsVar>();
        Parentvariables.Fired = Fired;




    }
    private void Start()
    {
        DamageDelay = 0.25f;
        TargetHP = 100;
        currentclip = MaxClip;
        totalammo = MaxAmmo;
        AmmoMessage = GameObject.Find("AMMO MESSAGE").GetComponent<TextMeshProUGUI>();
        Shootpoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;
        CameraMain = Camera.main.gameObject;   pos = CameraMain.transform.GetChild(2);


    }

    void Update()
    {//update S
     //var sync with plaayer
        Canfire = PlayerParent.GetComponent<PlayerActionsVar>().canfire;
        PlayerParent.GetComponent<WeaponStatus>().CurrentClip = currentclip;
        PlayerParent.GetComponent<WeaponStatus>().TotalAmmo = totalammo;


     


        modifiedFireRate = 1.0f / FireRate;

        if (TargetShield < 1 && TotalDamageDealt >= 100)//ResetTotalDamage
        {

            TotalDamageDealt = 0;

        }

        if (TargetHP == 0 && !hasExecutedKill && PV.IsMine)
        {
            if (LastDamageType == 0)
            {
                Kill();
                hasExecutedKill = true; // Set the flag to indicate the code has executed
            }
            else if (LastDamageType == 1)
                {
                   HeadKill();
                    hasExecutedKill = true; // Set the flag to indicate the code has executed
                }
        }

  
        if (Time.time > lastshot + 0.2f)
        {
            Fired = false;
           Parentvariables.Fired = false;
        }


        if (Canfire)
        { //canfire

            if (ButtonFired == true && PV.IsMine && Time.time > lastshot + modifiedFireRate && currentclip > 0 && !Reloading && Canfire)
            {
                AS.PlayOneShot(FireSFX, 1f);

                StartCoroutine(VFX());

                float nextActionTime = 0.0f;
                float period = 5f;
                if (Time.time > nextActionTime)
                {
                    nextActionTime += period;
                    Shoot();
                }
            }
        }//canfire


        else
        {
            return;
        }

        ///check reload conditions///

        //auto reload

        if (currentclip == 0 & !Reloading && !noammo && totalammo > 0)
        {
            StartCoroutine(Reload());
        }
        //Manual reload
        if (ButtonReload && !Reloading && !noammo && currentclip < MaxClip && totalammo > 0)
        {
            StartCoroutine(Reload());

        }


    }//update E


    void Shoot()

    {
        #region AtShootActions !!!! ^
        ScopeAnimator.SetTrigger("fired");
        started = true;
        Fired = true;
        Parentvariables.Fired = true;
        #endregion

        //track shots fired
        BulletsFired += 1;

        //subtract bullets
        currentclip--;

        //Reset FireRate

        lastshot = Time.time;

        //  SparkleVFX.SetActive(false);

        // Fire RayCast//
        Physics.Raycast(Shootpoint.position, pos.forward, out hit, WeaponRange, layermask);



        collided = hit.collider;

        point = (hit.point);
        started = false;

        //UpdateAmmoAfterShoot
        AmmoRefresh();


        if (collided == null || collided.gameObject.layer == 0)

        { return; }
        else

        { TPV = collided.transform.root.transform.GetChild(0).GetComponentInParent<PhotonView>();

            //bullet HOLE SPAWN 
            if (BulletHoleVFX != null)

            {
                GameObject.Instantiate(BulletHoleVFX, hit.point, transform.localRotation);
            }


            //Call Methods

            BodyShot();

            HeadShot();
            #region AfterShootActions
     
            #endregion
        }

        //check Bodyshot

        void BodyShot()


        { // SF


            if (collided != null && collided.name == "HIT BOX-BODY")


            {

                if (TPV != null)
                    //self shoot detect
                    if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
                        return;
                    else // other online player detect
                    {

                        //       TargetHP = TPV.GetComponent<TakeDamage>().HP;
                        //  TargetShield = TPV.GetComponent<TakeDamage>().Shield;

                        AS.PlayOneShot(BodyshotSFX, 1f);

                        RpcTarget RPCTYPE = new RpcTarget();
                        if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
                        {
                            RPCTYPE = RpcTarget.All;
                        }
                        else RPCTYPE = RpcTarget.Others;

                        Bodydamage();

                        //  TPV = collided.GetComponent<PhotonView>();

                        Debug.Log("Real Player Detected-Body");

                        HitReticleCrosshair.SetActive(true);
                    }



                else if (collided.name == "HIT BOX-BODY" && TPV == null)
                {
                    ///AI detct
                    if (collided.CompareTag("AI"))

                    {
                        TakeDamage takedamage = collided.transform.GetComponentInParent<TakeDamage>();

                        AS.PlayOneShot(BodyshotSFX, 1f);

                        Debug.Log("AI Target Detected-Body");

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);
                        takedamage.Takedamage(BodyDamage);

                    }

                    else
                    {

                        AS.PlayOneShot(BodyshotSFX, 1f);

                        Debug.Log("Iron Target Detected-Body");

                        TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                        if (takedamage != null)
                        { takedamage.Takedamage(BodyDamage); }

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);


                    }

                }

            }

            else return;

        } //EF
          //check headshot

        void HeadShot()


        { // SF


            if (collided != null && collided.name == "HIT BOX-HEAD")


            {

                if (TPV != null)
                    //self shoot detect
                    if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
                        return;
                    else // other online player detect
                    {

                        TargetHP = TPV.GetComponent<TakeDamage>().HP;
                        TargetShield = TPV.GetComponent<TakeDamage>().Shield;

                        AS.PlayOneShot(HeadshotSFX, 1f);

                        RpcTarget RPCTYPE = new RpcTarget();
                        if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
                        {
                            RPCTYPE = RpcTarget.All;
                        }
                        else RPCTYPE = RpcTarget.Others;

                        Headdamage();

                        //  TPV = collided.GetComponent<PhotonView>();

                        Debug.Log("Real Player Detected-HEAD");

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);
                    }



                else if (collided.name == "HIT BOX-HEAD" && TPV == null)
                {
                    ///AI detct
                    if (collided.CompareTag("AI"))

                    {
                        TakeDamage takedamage = collided.transform.GetComponentInParent<TakeDamage>();

                        AS.PlayOneShot(HeadshotSFX, 1f);

                        Debug.Log("AI Target Detected-HEAD");

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);
                        takedamage.Takedamage(HeadDamage);

                    }

                    else
                    {

                        AS.PlayOneShot(HeadshotSFX, 2.5f);

                        Debug.Log("Iron Target Detected-hEAD");

                        TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                        if (takedamage != null)
                        { takedamage.Takedamage(HeadDamage); }

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);


                    }

                }

            }

            else return;

        } //EF

    }//EF


    void Bodydamage()
    {      
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
        LastDamageType = 0;

        if (TargetHP > 0)
        {
            hasExecutedKill = false;
            TPV.RPC("Takedamage", RpcTarget.All, BodyDamage);
            StartCoroutine(UpdateTargetHP());
    
        }

        Debug.Log("body reached");
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
    }

    void Kill()
    {
        KillFeed.gameObject.SetActive(true);
        Parentvariables.TotalRoomkillsTrack++;

     
         TargetHP = 100;

        GameObject Killpopupitem = PhotonNetwork.Instantiate("KILLS POPUP ITEM", transform.position, Quaternion.identity); // spawn kill UI notification
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKilled = TPV.GetComponent<PhotonSerializerBGN>().PlayerNickName;
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKiller = PhotonNetwork.NickName;
        hasExecutedKill = false;
    }

    void HeadKill()
    {
        HeadShotKill.gameObject.SetActive(true);
        Parentvariables.TotalRoomkillsTrack++;


        TargetHP = 100;

        GameObject Killpopupitem = PhotonNetwork.Instantiate("KILLS POPUP ITEM", transform.position, Quaternion.identity); // spawn kill UI notification
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKilled = TPV.GetComponent<PhotonSerializerBGN>().PlayerNickName;
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKiller = PhotonNetwork.NickName;
        hasExecutedKill = false;
    }

    void Headdamage()
    {
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
        LastDamageType = 1;
        if (TargetHP > 0)
        {
            hasExecutedKill = false;
            TPV.RPC("Takedamage", RpcTarget.All, HeadDamage);
            StartCoroutine(UpdateTargetHP());

        }

        Debug.Log("Head reached");
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
    }


    public void AmmoRefil()
    {
        totalammo = MaxAmmo;  noammo = false;
        AmmoRefresh();
    }

    private void AmmoRefresh()
    {
        #region NO AMMO SET UP
        //NO AMMO SET UP

        if (weaponstatus.MaxedAmmo)
        {
            totalammo = MaxAmmo;
        }
        if (currentclip < 1 && totalammo == 0)
        {
            noammo = true; PlayerParent.GetComponent<WeaponStatus>().NoAmmo = true; AmmoMessage.color = Color.red; AmmoMessage.text = ("Out Of Ammo");
            BulletsFired = MaxClip;
        }

        else
        {
            {
                noammo = false; PlayerParent.GetComponent<WeaponStatus>().NoAmmo = false; AmmoMessage.text = ("");
            }
        }
        #endregion

        #region LOW AMMO SET UP
        if(totalammo ==0 && currentclip <MaxClip&&!noammo )
        {
            Lowammo = true;
            AmmoMessage.text = ("Low Ammo");
        }
        #endregion

        #region RESET BULLETS FIRED
        //Reset BulletsFired Custom conditions(calculate the difference manually)
        if (totalammo == 0)
        {
            BulletsFired = MaxClip - currentclip;
        }


        #endregion


        #region AMMO CLAMP
        if (totalammo <= 0)
        {
            totalammo = 0;
        }
        #endregion

        #region CLIP CLAMP
        //Clip Clamp
        if (currentclip <= 0)
        {
            currentclip = 0;
        }

        if (currentclip >= MaxClip)

        { currentclip = MaxClip; }
        #endregion
    }

    #region /////Coroutines/////

    //Ammo & reload
    IEnumerator Reload()
{
    Reloading = true;           PlayerParent.GetComponent<PlayerActionsVar>().IsReloading = true;

     noammo = false;

        AmmoMessage.color = Color.yellow;    AmmoMessage.text= ("Reloading..");  


                // Calculate reload time based on the inverse of WeaponType.ReloadSpeed
        float reloadTime = 1.0f / ReloadTime;

    yield return new WaitForSeconds(reloadTime);

    if (totalammo < MaxClip)
    {
        currentclip += totalammo;
        totalammo -= BulletsFired;
        BulletsFired = 0;
        Reloading = false;    
        AmmoRefresh();
        }
    else
    {
        currentclip += BulletsFired;
        totalammo -= BulletsFired;
        BulletsFired = 0;
        Reloading = false;                   PlayerParent.GetComponent<PlayerActionsVar>().IsReloading = false;
        AmmoMessage.text = (""); AmmoMessage.color = new Color(255f, 178f, 255f);
        AmmoRefresh();
        }


    }//ef
    //VFX Toggles
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.15f);

        BulletTrailVFX.Play();
        BulletDropVFX.Play();
    }
    // Hit Reticles Toggle


    IEnumerator UpdateTargetHP()
    {
        yield return new WaitForSeconds(DamageDelay);
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
   
        if (TargetShield <= 0)
        {
            TotalDamageDealt += BodyDamage;
        }
       


    }
    #endregion 
}//EC
