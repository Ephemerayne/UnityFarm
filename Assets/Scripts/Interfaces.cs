using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base actions
interface IEater 
{
    protected float Hunger { get; }
    void Eat(IEatable eatable);
}

interface IAnimalEater: IEater
{
    void Eat(IAnimalEatable animalEatable);
    // void Eat(IAnimalEatable animalEatable) 
    // {
    //     animalEatable.BeEaten();
    //     hunger = 1.0;
    // }
}

interface IPersonEater: IEater
{
    void Eat(IPersonEatable personEatable);
}

interface IEatable 
{
    // Съедается (исчезает)
    void BeEaten();
}
interface IAnimalEatable: IEatable { }
interface IPersonEatable: IEatable { }

interface IDrinker 
{
    void Drink(IDraggable drinkable);
}
interface IDrinkable
{
    // Выпивается (исчезает)
    void BeDrunk();
}

interface IPooper 
{
    void Poop(IPoopable poopable);
}
interface IPoopable {}

interface ISleeper 
{
    void Sleep(ISleepable sleepable);
}
interface IAnimalSleeper: ISleeper
{
    void Sleep(IAnimalSleepable animalSleepable);
}
interface IPersonSleeper: ISleeper
{
    void Sleep(IPersonSleepable personSleepable);
}

interface ISleepable { }
interface IAnimalSleepable { }
interface IPersonSleepable { }

interface IAttacker 
{
    void Attack(IAttackable attackable);
}
interface IAttackable 
{
    void TakeAttack(float damage);
}

interface IDragger {
    void Drag(IDraggable draggable);
    void Drop(IDraggable draggable, Vector2 position);
}
interface IDraggable {
    void TakeDrop(Vector2 position);
}

interface ICollectable { }
interface IStorable { }

interface IHandler {
    void Handle(IHandleable handleable);
}
interface IHandleable
{
    void TakeHandle();
}

interface IBarier { }

interface IDoorOpener {
    void Open(IDoor door);
}
interface IDoor {
    void TakeOpen();
    void TakeClose();
}

interface IMover { }
interface IGrowable {
    void Grow(/* delta time */);
}

// Game entities

interface IAnimal : IAnimalEater, IDrinker, IPooper, IAnimalSleeper, IMover { }

interface IFarmAnimal: IAnimal, ICollectable, IGrowable { }

interface IPetAnimal: IAnimal, IAttacker { }

interface IDraggerPetAnimal: IPetAnimal, IDragger { }

interface Person : IPersonEater, IDrinker, IPooper, IPersonSleeper, IDragger, IDoorOpener, IMover , IHandler { }
