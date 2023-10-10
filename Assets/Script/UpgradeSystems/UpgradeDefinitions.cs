using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum UpgradeType
{
    StandMultiplier,
    StandSpeedUp
}

public enum StandType
{
    All,
    Stand1,
    Stand2,
    Stand3
}
public enum GeneralUpgradeType
{
    CharacterSpeedUp,
    AllStandSpeedUp,
    AllMultiplier,
    AdBoost
}

public enum BodyType
{
    Head,
    Body,
    Pet
}
public class Upgrade
{
    public UpgradeType Type;
    public GeneralUpgradeType generalType;
    public StandType StandType;
    public float Value;
    public string Description;
}
