using ArduinoBridgeDemo.Arduino;
using System;
using System.Threading.Tasks;

namespace ArduinoBridgeDemo
{
    class Main
    {
        ArduinoBridge bridge = new ArduinoBridge() ;
        Pixel[] frame = new Pixel[12];
        Pixel p = new Pixel();

        Pixel red = new Pixel(20, 0, 0);
        Pixel green = new Pixel(0, 20, 0);
        Pixel blue = new Pixel(0, 0, 20);

        const int MinServoDegrees = 0;
        const int MaxServoDegrees = 140;
        const int TotalServoDegrees = MaxServoDegrees - MinServoDegrees;
        const int MidpointServoDegrees = TotalServoDegrees / 2;
        const int stepsSize = 2;


        private async void Servo()
        {
            int direction = stepsSize;
            int currentAngle = MidpointServoDegrees;
            int nextAngle = 0;

            bridge.ServoPin = 8;

            while (true)
            {
                nextAngle = CalculateNextAngle(currentAngle, ref direction);
                bridge.ServoPosition((ushort)nextAngle);
                await Task.Delay(120);
                currentAngle = nextAngle;
            }
        }

        private int CalculateNextAngle(int currentAngle, ref int direction)
        {
            if (currentAngle >= MaxServoDegrees) { direction = -stepsSize; }
            else if (currentAngle <= MinServoDegrees) { direction = stepsSize; }

            return currentAngle + direction;
        }

        async void PixelShow()
        {
            bridge.NeoPixelPin = 3;

            await Task.Yield();
            ClearFrame();

            Random rnd = new Random();

            while (true)
            {


                for (int i = 0; i < 3; i++)
                {
                    ClearFrame();
                    p = new Pixel();

                    for (byte r = 0; r < 15; r += 3)
                    {
                        switch (i)
                        {
                            case 0:
                                p.Red = r;
                                break;
                            case 1:
                                p.Green = r;
                                break;
                            case 2:
                                p.Blue = r;
                                break;
                            default:
                                break;
                        }

                        SetFrame(p);
                        bridge.FrameDraw(frame, 245);
                    }

                }


                for (int j = 0; j < 3; j++)
                {



                    for (int i = 0; i < frame.Length; i += 3)
                    {
                        frame[i] = red;
                    }

                    for (int i = 1; i < frame.Length; i += 3)
                    {
                        frame[i] = green;
                    }

                    for (int i = 2; i < frame.Length; i += 3)
                    {
                        frame[i] = blue;
                    }

                    bridge.FrameDraw(frame, 100);

                    for (int i = 0; i < frame.Length; i += 3)
                    {
                        frame[i] = green;
                    }

                    for (int i = 1; i < frame.Length; i += 3)
                    {
                        frame[i] = blue;
                    }

                    for (int i = 2; i < frame.Length; i += 3)
                    {
                        frame[i] = red;
                    }

                    bridge.FrameDraw(frame, 100);

                    for (int i = 0; i < frame.Length; i += 3)
                    {
                        frame[i] = blue;
                    }

                    for (int i = 1; i < frame.Length; i += 3)
                    {
                        frame[i] = red;
                    }

                    for (int i = 2; i < frame.Length; i += 3)
                    {
                        frame[i] = green;
                    }

                    bridge.FrameDraw(frame, 250);
                }
            }

        }


        public void Start()
        {
            Servo();
            PixelShow();
        }

        private void ClearFrame()
        {
            Pixel p = new Pixel(0);

            for (int i = 0; i < frame.Length; i++)
            {
                frame[i] = p;
            }
        }

        private void SetFrame(byte red, byte green, byte blue)
        {
            Pixel p = new Pixel(red, green, blue);

            for (int i = 0; i < frame.Length; i++)
            {
                frame[i] = p;
            }
        }

        private void SetFrame(Pixel p)
        {
            for (int i = 0; i < frame.Length; i++)
            {
                frame[i] = p;
            }
        }
    }
}
