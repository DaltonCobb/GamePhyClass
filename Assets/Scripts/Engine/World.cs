using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsText;
    public VectorField vectorField;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();
    public List<Spring> springs { get; set; } = new List<Spring>();
    public List<Force> forces { get; set; } = new List<Force>();
    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    float fps = 0;
    float timeAccumulator;
    float fpsAverage = 0;
    float smoothing = 0.975f;

    public Vector2 WorldSize { get => size * 2; }
    public AABB AABB { get => aabb; }

    AABB aabb;
    Vector2 size;
  
    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        aabb = new AABB(Vector2.zero, size * 2);
    }
    void Update()
    {
        float dt = Time.deltaTime;
        fps = (1.0f / dt);
       
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAverage.ToString("F1");

        springs.ForEach(spring => spring.Draw());

        if (!simulate) return;

        GravitationalForce.ApplyForce(bodies, gravitation);
        forces.ForEach(force => bodies.ForEach(body => force.ApplyForce(body)));
        springs.ForEach(spring => spring.ApplyForce());
        bodies.ForEach(body => vectorField.ApplyForce(body));

        timeAccumulator = timeAccumulator + Time.deltaTime;
        while (timeAccumulator >= fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Intergrator.SemiImplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);

            if(collision == true)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
                ContactSolver.Reslove(contacts);
            }

            timeAccumulator = timeAccumulator - fixedDeltaTime;
        }
        if (wrap)
        {
            bodies.ForEach(body => body.position = Utillites.Wrap(body.position, -size, size));

        }
            bodies.ForEach(body => body.force = Vector2.zero);
            bodies.ForEach(body => body.acceleration = Vector2.zero);
        
    }
}
