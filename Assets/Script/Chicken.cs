﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Chicken {

    public const int MAX_HUNGER_STATUS = 60; // hunger status increase by 1 every minute
    public float hungerStatus { get; private set; }
    public float deathProbability { get; private set; }
    public int id { get; private set; }
    public string name { get; private set; }
    public int age { get; private set; }
    public int marketValue { get; private set; }
    public float eggFrequency { get; private set; }
    public Grade grade { get; private set; }
    public bool canProduceEgg { get; private set; }
    public bool producedEgg { get; private set; }
    public bool dead { get; set; }

    // used by chicken and egg
    public enum Grade
    {
        A,
        B,
        C,
        S
    }
    DateTime produceEggStartTime;
    DateTime lastAgeingTime;

    // called when chicken is borned
    public void Initialize(int id, Grade grade, string name = "")
    {
        this.id = id;
        this.grade = grade;
        this.name = name; // empty string if not specified
        this.age = 0;
        this.hungerStatus = 0; // not hungry
        this.deathProbability = 0; // because age is 0
        this.canProduceEgg = false; // because age is 0
        this.producedEgg = false;
        this.dead = false;

        switch (this.grade)
        {
            case Grade.A:
                eggFrequency = 30;
                marketValue = 200;
                break;
            case Grade.B:
                eggFrequency = 60;
                marketValue = 400;
                break;
            case Grade.C:
                eggFrequency = 90;
                marketValue = 600;
                break;
            case Grade.S:
                eggFrequency = 120;
                marketValue = 800;
                break;
        }
    }

    // called when chicken is transferred or bought
    public void Initialize(int id, Grade grade, int age, string name = "")
    {
        this.id = id;
        this.grade = grade;
        this.name = name; // empty string if not specified
        this.age = age;
        this.hungerStatus = 0; // not hungry
        this.deathProbability = (age >= 10 ? (age - 10) / 10 : 0);
        this.canProduceEgg = age >= 3 ? true : false;
        this.producedEgg = false;
        this.dead = false;

        switch (this.grade)
        {
            case Grade.A:
                eggFrequency = 30;
                marketValue = 200;
                break;
            case Grade.B:
                eggFrequency = 60;
                marketValue = 400;
                break;
            case Grade.C:
                eggFrequency = 90;
                marketValue = 600;
                break;
            case Grade.S:
                eggFrequency = 120;
                marketValue = 800;
                break;
        }
    }

    // chicken lifecycle loop
    public void StayAlive()
    {
        ProduceEgg();
        IncreaseHungerStatus();
        Ageing();
    }

    public void ProduceEgg()
    {
        if (!canProduceEgg)
            return;

        if (!producedEgg && (DateTime.UtcNow.Hour - produceEggStartTime.Hour) >= 1)
        {
            OnFinishedProduceEgg();
        }
    }

    public void ProduceEggOffline()
    {

    }

    void OnFinishedProduceEgg()
    {
        producedEgg = true;
        // transfer egg to warehouse
        WarehouseController.instance.StoreEggs(1, grade);

    }

    public void Ageing()
    {
        if ((DateTime.UtcNow.Hour - lastAgeingTime.Hour) >= 12)
        {
            age++;
            if (age == 3)
            {
                produceEggStartTime = DateTime.UtcNow;
                canProduceEgg = true;
            }
        }
    }

    public void AgeingOffline()
    {

    }

    public void IncreaseHungerStatus()
    {
        hungerStatus++;
    }

    public void IncreaseHungerStatusOffline()
    {

    }

    public void Feed()
    {
        hungerStatus = 0;
    }
}
