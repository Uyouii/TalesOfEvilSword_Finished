#pragma strict

private var magicParticlesRotation : float = 10.0;
public var mprSpeed : float = 1.75;

function Start () {

}

function Update () {

     transform.Rotate(0,mprSpeed,0 * magicParticlesRotation * Time.deltaTime,0);
}



