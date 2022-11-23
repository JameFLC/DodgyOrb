using UnityEngine;

namespace DodgyOrb
{
    public class PontController : MonoBehaviour
    {
        [SerializeField] private Transform pont1, pont2, pont3;
        [SerializeField] private int buttonIsPressed = 1;
        [SerializeField] private float speed = 0.01f;

        private AudioSource bridgeSoundFlip;

        // Start is called before the first frame update
        void Start()
        {
            pont1.rotation = Quaternion.Euler(-20, 0, 0);
            pont2.rotation = Quaternion.Euler(20, 0, 0);
            pont3.rotation = Quaternion.Euler(-20, 0, 0);

            bridgeSoundFlip = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

            if (buttonIsPressed == 1)
            {
                
                pont1.rotation = Quaternion.Lerp(pont1.rotation, Quaternion.Euler(20, 0, 0), Time.deltaTime * speed);
                pont2.rotation = Quaternion.Lerp(pont2.rotation, Quaternion.Euler(-20, 0, 0), Time.deltaTime * speed);
                pont3.rotation = Quaternion.Lerp(pont3.rotation, Quaternion.Euler(20, 0, 0), Time.deltaTime * speed);
            }
            else
            {
                pont1.rotation = Quaternion.Lerp(pont1.rotation, Quaternion.Euler(-20, 0, 0), Time.deltaTime * speed);
                pont2.rotation = Quaternion.Lerp(pont2.rotation, Quaternion.Euler(20, 0, 0), Time.deltaTime * speed);
                pont3.rotation = Quaternion.Lerp(pont3.rotation, Quaternion.Euler(-20, 0, 0), Time.deltaTime * speed);
            }
        }

        public void GetData(Vector2 data)
        {
            bridgeSoundFlip.Play();
            buttonIsPressed = (int)data.x;
        }
    }
}
