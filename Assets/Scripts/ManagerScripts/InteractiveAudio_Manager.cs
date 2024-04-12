using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAudio_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static InteractiveAudio_Manager _Instance { get; private set; } = null;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
    }
    #endregion

    #region Sound Variables
    private AudioSource _audioSource;
    public AudioClip _coreSoundClip;
    public AudioClip _buildSiteSoundClip;
    public AudioClip _mineSoundClip;
    public AudioClip _staticCollectorSoundClip;
    public AudioClip _factorySoundClip;
    public AudioClip _turretSoundClip;
    public AudioClip _workerSoundClip;
    public AudioClip _meleeSoundClip;
    public AudioClip _rangedSoundClip;
    public AudioClip _metalSoundClip;
    public AudioClip _staticSoundClip;
    public AudioClip _platinumSoundClip;
    public AudioClip _outpostSoundClip;
    public AudioClip _enemyMeleeSoundClip;
    public AudioClip _enemyRangedSoundClip;
    public AudioClip _turretFireSoundClip;
    public AudioClip _meleeAttackSoundClip;
    public AudioClip _rangedAttackSoundClip;
    public AudioClip _workerHarvestOreAudioClip;
    public AudioClip _workerHarvestStaticAudioClip;
    public AudioClip _unitDeathAudioClip;
    public AudioClip _buildingDeathAudioClip;
    public AudioClip _busIncrementAudioClip;
    public AudioClip _busDecrementAudioClip;
    public AudioClip _busCompleteAudioClip;
    public AudioClip _victoryAudioClip;
    public AudioClip _defeatAudioClip;
    #endregion

    #region Can Play Clip
    bool _canPlayWorker;
    bool _canPlayMelee;
    bool _canPlayRanged;
    bool _canPlayHarvest;
    #endregion

    #region Particle Systems
    public ParticleSystem _unitDeathPrefab;
    public ParticleSystem _meleeAttackPrefab;
    public ParticleSystem _enemyMeleeAttackPrefab;
    public ParticleSystem _buildingDestroyedPrefab;
    public ParticleSystem _rangedAttackPrefab;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _canPlayWorker = true;
        _canPlayRanged = true;
        _canPlayMelee = true;
        _canPlayHarvest = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayInteractiveSound(string tag, string childTag)
    {
        if (tag.Equals(TagList.CORE))
        {
            _audioSource.PlayOneShot(_coreSoundClip);
        }
        else if (tag.Equals(TagList.OUTPOST))
        {
            _audioSource.PlayOneShot(_outpostSoundClip);
        }
        else if (tag.Equals(TagList.MINE))
        {
            _audioSource.PlayOneShot(_mineSoundClip);
        }
        else if (tag.Equals(TagList.STATIC_COLLECTOR))
        {
            _audioSource.PlayOneShot(_staticCollectorSoundClip);
        }
        else if (tag.Equals(TagList.FACTORY))
        {
            _audioSource.PlayOneShot(_factorySoundClip);
        }
        else if (tag.Equals(TagList.TURRET))
        {
            _audioSource.PlayOneShot(_turretSoundClip);
        }
        else if (tag.Equals(TagList.BUILD_SITE))
        {
            _audioSource.PlayOneShot(_buildSiteSoundClip);
        }
        // CHECK IF CAN PLAY
        else if (tag.Equals(TagList.WORKER))
        {
            if (_canPlayWorker)
            {
                _canPlayWorker = false;
                StartCoroutine(enforceSingleClipPlaying(_workerSoundClip.length, TagList.WORKER));
                _audioSource.PlayOneShot(_workerSoundClip);
            }
        }
        else if (tag.Equals(TagList.MELEE_UNIT))
        {
            if (_canPlayMelee)
            {
                _canPlayMelee = false;
                StartCoroutine(enforceSingleClipPlaying(_meleeSoundClip.length, TagList.MELEE_UNIT));
                _audioSource.PlayOneShot(_meleeSoundClip);
            }
        }
        else if (tag.Equals(TagList.RANGED_UNIT))
        {
            if (_canPlayRanged)
            {
                _canPlayRanged = false;
                StartCoroutine(enforceSingleClipPlaying(_rangedSoundClip.length, TagList.RANGED_UNIT));
                _audioSource.PlayOneShot(_rangedSoundClip);
            }
        }
        // END CHECK
        else if (tag.Equals(TagList.METAL))
        {
            _audioSource.PlayOneShot(_metalSoundClip);
        }
        else if (tag.Equals(TagList.STATIC))
        {
            _audioSource.PlayOneShot(_staticSoundClip);
        }
        else if (tag.Equals(TagList.PLATINUM))
        {
            _audioSource.PlayOneShot(_platinumSoundClip);
        }
        else if (tag.Equals(TagList.OUTPOST))
        {
            _audioSource.PlayOneShot(_outpostSoundClip);
        }
        else if (tag.Equals(TagList.ENEMY_UNIT))
        {
            if (childTag.Equals(TagList.MELEE_UNIT))
            {
                _audioSource.PlayOneShot(_enemyMeleeSoundClip);
            }
            else if (childTag.Equals(TagList.RANGED_UNIT))
            {
                _audioSource.PlayOneShot(_enemyRangedSoundClip);
            }
        }
        else if (tag.Equals(TagList.ENEMY_BUILDING))
        {
            if (childTag.Equals(TagList.CORE))
            {
                _audioSource.PlayOneShot(_coreSoundClip);
            }
            else if (childTag.Equals(TagList.OUTPOST))
            {
                _audioSource.PlayOneShot(_outpostSoundClip);
            }
            else if (childTag.Equals(TagList.MINE))
            {
                _audioSource.PlayOneShot(_mineSoundClip);
            }
            else if (childTag.Equals(TagList.STATIC_COLLECTOR))
            {
                _audioSource.PlayOneShot(_staticCollectorSoundClip);
            }
            else if (childTag.Equals(TagList.FACTORY))
            {
                _audioSource.PlayOneShot(_factorySoundClip);
            }
            else if (childTag.Equals(TagList.TURRET))
            {
                _audioSource.PlayOneShot(_turretSoundClip);
            }
        }
    }
    
    private IEnumerator enforceSingleClipPlaying(float clipTime, string tag)
    {
        yield return new WaitForSeconds(clipTime);
        if (tag.Equals(TagList.WORKER))
        {
            _canPlayWorker = true;
        } else if (tag.Equals(TagList.MELEE_UNIT))
        {
            _canPlayMelee = true;
        } else if (tag.Equals(TagList.RANGED_UNIT))
        {
            _canPlayRanged = true;
        }
    }

    public void turretFire()
    {
        _audioSource.PlayOneShot(_turretFireSoundClip);
    }

    public void meleeFire(GameObject gameObject)
    {
        if (gameObject.transform.parent.tag.Equals(TagList.ENEMY_UNIT) || gameObject.transform.parent.tag.Equals(TagList.ENEMY_BUILDING))
        {
            ParticleSystem meleeBurst = Instantiate(_meleeAttackPrefab, gameObject.transform.parent.transform.position, Quaternion.identity);
            meleeBurst.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        } else
        {
            ParticleSystem meleeBurst = Instantiate(_enemyMeleeAttackPrefab, gameObject.transform.parent.transform.position, Quaternion.identity);
            meleeBurst.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        }
        _audioSource.PlayOneShot(_meleeAttackSoundClip);
    }

    public void rangedFire(GameObject gameObject)
    {
        ParticleSystem rangedLaser = Instantiate(_rangedAttackPrefab, gameObject.transform.parent.transform.position, Quaternion.identity);
        rangedLaser.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        _audioSource.PlayOneShot(_rangedAttackSoundClip);
    }

    public void workerHarvestOre()
    {
        if (_canPlayHarvest)
        {
            _canPlayHarvest = false;
            StartCoroutine(enforceHarvestAudioClip(_workerHarvestOreAudioClip.length));
            _audioSource.PlayOneShot(_workerHarvestOreAudioClip);
        }
    }

    public void workerHarvestStatic()
    {
        if (_canPlayHarvest)
        {
            _canPlayHarvest = false;
            StartCoroutine(enforceHarvestAudioClip(_workerHarvestStaticAudioClip.length));
            _audioSource.PlayOneShot(_workerHarvestStaticAudioClip);
        }
    }

    public void busIncrement()
    {
        _audioSource.PlayOneShot(_busIncrementAudioClip);
    }

    public void busDecrement()
    {
        _audioSource.PlayOneShot(_busDecrementAudioClip);
    }

    public void busComplete()
    {
        _audioSource.PlayOneShot(_busCompleteAudioClip);
    }

    public void unitDeath(GameObject deadUnit)
    {
        ParticleSystem explosion = Instantiate(_unitDeathPrefab, deadUnit.transform.parent.transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(.3f, .3f, 0);
        _audioSource.PlayOneShot(_unitDeathAudioClip);
    }

    public void buildingDeath(GameObject destroyedBuilding)
    {
        ParticleSystem explosion = Instantiate(_buildingDestroyedPrefab, destroyedBuilding.transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(.3f, .3f, 0);
        _audioSource.PlayOneShot(_buildingDeathAudioClip);
    }
    public void victory()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
        _audioSource.PlayOneShot(_victoryAudioClip);
    }

    public void defeat()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
        _audioSource.PlayOneShot(_defeatAudioClip);
    }

    public void genericButton()
    {
        _audioSource.PlayOneShot(_metalSoundClip);
    }

    private IEnumerator enforceHarvestAudioClip(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        _canPlayHarvest = true;
    }
}
