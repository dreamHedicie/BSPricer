﻿using System;

namespace Pricer
{
    public class NormalCdfComputer : INormalCdfComputer
    {
        private static readonly double[] _c = {
            1.27109764952614E-03 + 9.2E-19,
            1.1931402283834E-04 + 9.44E-19,
            3.96385097360513E-03 + 5E-18,
            8.70779635317295E-04 + 8.28E-19,
            7.73672528313526E-03 + 6.68E-18,
            3.83335126264887E-03 + 3.03E-18,
            1.27223813782122E-02 + 7.55E-17,
            0.013382364453346 + 6.9E-18,
            1.61315329733252E-02 + 2.48E-17,
            3.90976845588484E-02 + 3.5E-18,
            2.49367200053503E-03 + 3.04E-18,
            8.38864557023001E-02 + 9.92E-17,
            0.119463959964325 + 4.15E-16,
            1.66207924969367E-02 + 3.56E-17,
            0.357524274449531 + 4.3E-17,
            0.80527640875291 + 5.67E-16,
            1.18902982909273 + 3.33E-15,
            1.37040217682338 + 1.67E-15,
            1.31314653831023 + 9.8E-16,
            1.07925515155856 + 6.77E-15,
            0.774368199119538 + 6.09E-16,
            0.490165080585318 + 4.24E-16,
            0.275374741597376 + 7.82E-16
        };
        
        public double Compute(double d1)
        {
            return ComputeErfc(-d1 / Math.Pow(2, 0.5)) * 0.5;
        }

        private double ComputeErfc(double d)
        {
            var t = 3.97886080735226 / (Math.Abs(d) + 3.97886080735226);
            var u = t - 0.5;
            var y = _c[0] * u + _c[1];
            y = y * u - _c[2];
            y = y * u - _c[3];
            y = y * u + _c[4];
            y = y * u + _c[5];
            y = y * u - _c[6];
            y = y * u - _c[7];
            y = y * u + _c[8];
            y = y * u + _c[9];
            y = y * u + _c[10];
            y = y * u - _c[11];
            y = y * u - _c[12];
            y = y * u + _c[13];
            y = y * u + _c[14];
            y = y * u + _c[15];
            y = y * u + _c[16];
            y = y * u + _c[17];
            y = y * u + _c[18];
            y = y * u + _c[19];
            y = y * u + _c[20];
            y = y * u + _c[21];
            y = y * u + _c[22];
            y = y * t * Math.Exp(-d * d);
            if (d < 0)
            {
                return 2 - y;
            }

            return y;
        }
    }
}