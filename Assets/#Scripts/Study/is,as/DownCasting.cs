using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCasting : MonoBehaviour
{
    // C++ 의 다운캐스팅과 관련된 유니티 제공 연산자 is,as 

    // is : 객체가 해당 형싟에 해당하는지 검사하여, 결과를 bool 값으로 반환.
    // as : 형식 변환 연사자와 같은 역할. but 형변환 연산자는 변환 실패시 예외를 던지지만, as 연산자는 객체 참조를 null 로 만든다.

    public class Mammal // mammal = 포유류
    {
     public void Nurse()
        {

        }
    }
    class Dog : Mammal
    {
        public void Bark(){}
        
    }
    class Cat : Mammal
    {
        public void Meow(){}
    }

    void normalDown()
    {
        Mammal m = new Mammal();
        m.Nurse();

        m = new Dog();
        m.Nurse();

        Dog dog = (Dog)m; // 다운캐스팅
        dog.Nurse(); // 하지만 m 이 가리키고 있는 것이
        dog.Bark(); // Dog 이기에 가능하다.

        m = new Cat();
        m.Nurse();

        Cat cat = (Cat)m;
        cat.Nurse();
        cat.Meow();
    }

    void IsAs()
    {
        Mammal m = new Dog();
        Dog dog;

        if (m is Dog) // m 이 Dog 을 가리키고 있나?
        {
            dog = (Dog)m; // 다운캐스팅
            dog.Bark();
        }


        Mammal m2 = new Cat();
        Cat cat = m2 as Cat; // m2를 Cat 으로 형변환하라
        if(cat != null) // m2 는 Cat 을 가리키고 있기 때문에, 다운캐스팅을 하지 않아도 가능하다. m2 = new Mammal() 이었다면 불가능.
        {
            cat.Meow();
        }

    }
    private void Start()
    {

    }
}
