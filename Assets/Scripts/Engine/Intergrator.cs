using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Intergrator
{
    public static void ExplicitEuler(Body body, float dt)
    {
        body.acceleration = (body.force / body.mass);
        body.velocity = body.velocity + body.acceleration * Time.deltaTime;
        body.position = body.position + body.velocity * Time.deltaTime;

       // body.position = body.position + (body.velocity * dt);
       // body.velocity = body.velocity + (body.acceleration * dt);
       // body.velocity = body.velocity * (1.0f / (1.0f + (body.damping * dt)));
    }
}
