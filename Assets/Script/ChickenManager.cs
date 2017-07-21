using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenManager : MonoBehaviour {

    public static ChickenManager instance;
    // for display purposes
    public List<Chicken> Chicken_GradeA { get; private set; }
    public List<Chicken> Chicken_GradeB { get; private set; }
    public List<Chicken> Chicken_GradeC { get; private set; }
    public List<Chicken> Chicken_GradeS { get; private set; }
    // for searching purposes
    public Dictionary<int, Chicken> ChickenDictionary { get; private set; }

    public List<ChickenFeed> Feed_GradeA { get; private set; }
    public List<ChickenFeed> Feed_GradeB { get; private set; }
    public List<ChickenFeed> Feed_GradeC { get; private set; }
    public List<ChickenFeed> Feed_GradeS { get; private set; }

    int latest_acquired_id; // id starts from 0
    float feedingFrequency; // how often should it feed the chickens

    DateTime lastLifeSimulationTime;
    DateTime lastFeedTime;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Chicken_GradeA = new List<Chicken>();
        Chicken_GradeB = new List<Chicken>();
        Chicken_GradeC = new List<Chicken>();
        Chicken_GradeS = new List<Chicken>();
        ChickenDictionary = new Dictionary<int, Chicken>();
    }

    void Update()
    {
        SimulateChickenLifeEvents();
        FeedChickens();
    }

    // for newly born chicken (age 0)
    // chicken acquisation should be done in ChickenManager and not in BarnController
    public void InitializeChicken(Chicken.Grade grade, string name = "")
    {
        Chicken chicken = new Chicken();
        chicken.Initialize(latest_acquired_id, grade, name);
        
        switch (grade)
        {
            case Chicken.Grade.A:
                Chicken_GradeA.Add(chicken);
                break;
            case Chicken.Grade.B:
                Chicken_GradeB.Add(chicken);
                break;
            case Chicken.Grade.C:
                Chicken_GradeC.Add(chicken);
                break;
            case Chicken.Grade.S:
                Chicken_GradeS.Add(chicken);
                break;
        }

        ChickenDictionary.Add(latest_acquired_id, chicken);
        latest_acquired_id++;

        // add this chicken to barn
        BarnController.instance.AddChicken(chicken);
    }

    // for chicken with an age
    // chicken acquisation should be done in ChickenManager and not in BarnController
    public void InitializeChicken(Chicken.Grade grade, int age, string name="")
    {
        Chicken chicken = new Chicken();
        chicken.Initialize(latest_acquired_id, grade, age, name);

        switch (grade)
        {
            case Chicken.Grade.A:
                Chicken_GradeA.Add(chicken);
                break;
            case Chicken.Grade.B:
                Chicken_GradeB.Add(chicken);
                break;
            case Chicken.Grade.C:
                Chicken_GradeC.Add(chicken);
                break;
            case Chicken.Grade.S:
                Chicken_GradeS.Add(chicken);
                break;
        }

        ChickenDictionary.Add(latest_acquired_id, chicken);
        latest_acquired_id++;

        // add this chicken to barn
        BarnController.instance.AddChicken(chicken);
    }

    // chicken lifecycle
    void SimulateChickenLifeEvents()
    {
        // run this lifecycle loop every minute instead of every frame
        if (Math.Abs(DateTime.UtcNow.Minute - lastLifeSimulationTime.Minute) < 1)
            return;

        foreach (KeyValuePair<int, Chicken> entry in ChickenDictionary)
        {
            entry.Value.StayAlive();
        }
    }

    void SimulateChickenLifeEventsOffline()
    {
        
    }

    void FeedChickens()
    {
        // only feed chicken every hour
        if (DateTime.UtcNow.Hour - lastFeedTime.Hour < 1)
        {
            return;
        }

        lastFeedTime = DateTime.UtcNow;

        // feed grade C chickens
        foreach(Chicken chicken in Chicken_GradeC)
        {
            if (Feed_GradeC.Count > 0)
            {
                Feed_GradeC.RemoveAt(Feed_GradeC.Count - 1);
                chicken.Feed();
                continue;
            }

            if (Feed_GradeB.Count > 0)
            {
                Feed_GradeB.RemoveAt(Feed_GradeB.Count - 1);
                chicken.Feed();
                continue;
            }
            if (Feed_GradeA.Count > 0)
            {
                Feed_GradeA.RemoveAt(Feed_GradeA.Count - 1);
                chicken.Feed();
                continue;
            }
            if (Feed_GradeS.Count > 0)
            {
                Feed_GradeS.RemoveAt(Feed_GradeS.Count - 1);
                chicken.Feed();
                continue;
            }
        }

        // feed grade B chicken
        foreach (Chicken chicken in Chicken_GradeB)
        {
            if (Feed_GradeB.Count > 0)
            {
                Feed_GradeB.RemoveAt(Feed_GradeB.Count - 1);
                chicken.Feed();
                continue;
            }

            if (Feed_GradeA.Count > 0)
            {
                Feed_GradeA.RemoveAt(Feed_GradeA.Count - 1);
                chicken.Feed();
                continue;
            }
            if (Feed_GradeS.Count > 0)
            {
                Feed_GradeS.RemoveAt(Feed_GradeS.Count - 1);
                chicken.Feed();
                continue;
            }
        }

        // feed grade A chicken
        foreach (Chicken chicken in Chicken_GradeA)
        {
            if (Feed_GradeA.Count > 0)
            {
                Feed_GradeA.RemoveAt(Feed_GradeA.Count - 1);
                chicken.Feed();
                continue;
            }

            if (Feed_GradeS.Count > 0)
            {
                Feed_GradeS.RemoveAt(Feed_GradeS.Count - 1);
                chicken.Feed();
                continue;
            }
        }

        // feed grade S chicken
        foreach (Chicken chicken in Chicken_GradeS)
        {
            if (Feed_GradeS.Count > 0)
            {
                Feed_GradeS.RemoveAt(Feed_GradeS.Count - 1);
                chicken.Feed();
                continue;
            }
        }

    }

    void FeedChickensOffline()
    {

    }

    // calculates whether if this chicken should be dead at this age
    void EvaluateChickenDeath()
    {
        float rand = 0;

        foreach (KeyValuePair<int, Chicken> entry in ChickenDictionary)
        {
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand <= entry.Value.deathProbability)
            {
                entry.Value.dead = true;
            }
        }
    }

    public int GetBarnChickenCount()
    {
        return BarnController.instance.ChickenCount;
    }

    public int GetBarnAndHatchingChickenCount()
    {
        return BarnController.instance.ChickenCount + HatcheryController.instance.hatchingCount;
    }

    public int GetChickenCountOfType(Chicken.Grade grade)
    {
        switch (grade)
        {
            case Chicken.Grade.A:
                return Chicken_GradeA.Count;
            case Chicken.Grade.B:
                return Chicken_GradeB.Count;
            case Chicken.Grade.C:
                return Chicken_GradeC.Count;
            case Chicken.Grade.S:
                return Chicken_GradeS.Count;
            default:
                return -1;
        }
    }

    public bool RemoveChicken(int id, Chicken.Grade grade)
    {
        List<Chicken> list = null;

        switch(grade)
        {
            case Chicken.Grade.A:
                list = Chicken_GradeA;
                break;
            case Chicken.Grade.B:
                list = Chicken_GradeB;
                break;
            case Chicken.Grade.C:
                list = Chicken_GradeC;
                break;
            case Chicken.Grade.S:
                list = Chicken_GradeS;
                break;
        }

        foreach(Chicken c in list)
        {
            if (c.id == id)
            {
                list.Remove(c);
                ChickenDictionary.Remove(id);
                return true;
            }
        }
        return false;
    }
}
