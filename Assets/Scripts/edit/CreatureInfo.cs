using System.Collections;
using System.Collections.Generic;
public class CreatureInfo {
    public int rank;
    public string name;
    public Creature.ActionState currState;
    public float currHealth;
    public float currHungry;
    public float currAge;
    public CreatureInfo(int rank, string name, Creature.ActionState state, float health, float hungry, float age) {
        this.rank = rank;
        this.name = name;
        currState = state;
        currHealth = health;
        currHungry = hungry;
        currAge = age;
    }
}