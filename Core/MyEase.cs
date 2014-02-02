/// Adapted from Robert Penner's ease functions
/// Access robertpenner.com for more informations

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mine {

    public static class MyEase {

        #region enum

        public enum EaseTypes {
            Linear,
            InQuad, OutQuad, InOutQuad,
            InCubic, OutCubic, InOutCubic,
            InQuart, OutQuart, InOutQuart,
            InQuint, OutQuint, InOutQuint,
            InSine, OutSine, InOutSine,
            InExpo, OutExpo, InOutExpo,
            InCirc, OutCirc, InOutCirc,
            InElastic, OutElastic, InOutElastic,
            InBack, OutBack, InOutBack,
            InBounce, OutBounce, InOutBounce
        }

        #endregion

        #region value

        public static float GetValue(EaseTypes type, float time, float duration, float initial, float final) {
            float value = 0;
            switch(type) {
                case EaseTypes.Linear: value = MyEase.Linear(time,duration,initial,final); break;
                case EaseTypes.InQuad: value = MyEase.InQuad(time,duration,initial,final); break;
                case EaseTypes.OutQuad: value = MyEase.OutQuad(time,duration,initial,final); break;
                case EaseTypes.InOutQuad: value = MyEase.InOutQuad(time,duration,initial,final); break;
                case EaseTypes.InCubic: value = MyEase.InCubic(time,duration,initial,final); break;
                case EaseTypes.OutCubic: value = MyEase.OutCubic(time,duration,initial,final); break;
                case EaseTypes.InOutCubic: value = MyEase.InOutCubic(time,duration,initial,final); break;
                case EaseTypes.InQuart: value = MyEase.InQuart(time,duration,initial,final); break;
                case EaseTypes.OutQuart: value = MyEase.OutQuart(time,duration,initial,final); break;
                case EaseTypes.InOutQuart: value = MyEase.InOutQuart(time,duration,initial,final); break;
                case EaseTypes.InQuint: value = MyEase.InQuint(time,duration,initial,final); break;
                case EaseTypes.OutQuint: value = MyEase.OutQuint(time,duration,initial,final); break;
                case EaseTypes.InOutQuint: value = MyEase.InOutQuint(time,duration,initial,final); break;
                case EaseTypes.InSine: value = MyEase.InSine(time,duration,initial,final); break;
                case EaseTypes.OutSine: value = MyEase.OutSine(time,duration,initial,final); break;
                case EaseTypes.InOutSine: value = MyEase.InOutSine(time,duration,initial,final); break;
                case EaseTypes.InExpo: value = MyEase.InExpo(time,duration,initial,final); break;
                case EaseTypes.OutExpo: value = MyEase.OutExpo(time,duration,initial,final); break;
                case EaseTypes.InOutExpo: value = MyEase.InOutExpo(time,duration,initial,final); break;
                case EaseTypes.InCirc: value = MyEase.InCirc(time,duration,initial,final); break;
                case EaseTypes.OutCirc: value = MyEase.OutCirc(time,duration,initial,final); break;
                case EaseTypes.InOutCirc: value = MyEase.InOutCirc(time,duration,initial,final); break;
                case EaseTypes.InElastic: value = MyEase.InElastic(time,duration,initial,final,0,0); break;
                case EaseTypes.OutElastic: value = MyEase.OutElastic(time,duration,initial,final,0,0); break;
                case EaseTypes.InOutElastic: value = MyEase.OutElastic(time,duration,initial,final,0,0); break;
                case EaseTypes.InBack: value = MyEase.InBack(time,duration,initial,final,0); break;
                case EaseTypes.OutBack: value = MyEase.OutBack(time,duration,initial,final,0); break;
                case EaseTypes.InOutBack: value = MyEase.InOutBack(time,duration,initial,final,0); break;
                case EaseTypes.InBounce: value = MyEase.InBounce(time,duration,initial,final); break;
                case EaseTypes.OutBounce: value = MyEase.OutBounce(time,duration,initial,final); break;
                case EaseTypes.InOutBounce: value = MyEase.InOutBounce(time,duration,initial,final); break;
            }
            return value;
        }

        #endregion

        #region functions

        #region linear

        public static float Linear(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return (delta * time) / duration + initial;
        }

        #endregion

        #region quad

        public static float InQuad(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Pow(time / duration, 2) + initial;
        }

        public static float OutQuad(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return (-1 * delta) * (time /= duration) * (time - 2) + initial;
        }

        public static float InOutQuad(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return delta / 2 * (float)Math.Pow(time, 2) + initial;
            return (-1 * delta) / 2 * ((--time) * (time - 2) - 1) + initial;
        }

        #endregion

        #region cubic

        public static float InCubic(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * ((float)Math.Pow(time / duration, 3)) + initial;
        }

        public static float OutCubic(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * ((float)Math.Pow(time / duration - 1, 3) + 1) + initial;
        }

        public static float InOutCubic(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return delta / 2 * (float)Math.Pow(time, 3) + initial;
            return delta / 2 * (float)(Math.Pow(time - 2, 3) + 2) + initial;
        }

        #endregion

        #region quart

        public static float InQuart(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Pow(time / duration, 4) + initial;
        }

        public static float OutQuart(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return (-1 * delta) * (float)(Math.Pow(time / duration - 1, 4) - 1) + initial;
        }

        public static float InOutQuart(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return delta / 2 * (float)Math.Pow(time, 4) + initial;
            return (-1 * delta / 2) * (float)(Math.Pow(time - 2, 4) - 2) + initial;
        }

        #endregion

        #region quint

        public static float InQuint(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Pow(time / duration, 5) + initial;
        }

        public static float OutQuint(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)(Math.Pow(time / duration - 1, 5) + 1) + initial;
        }

        public static float InOutQuint(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return delta / 2 * (float)Math.Pow(time, 5) + initial;
            return delta / 2 * (float)(Math.Pow(time - 2, 5) + 2) + initial;
        }

        #endregion

        #region sine

        public static float InSine(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (1 - (float)Math.Cos(time / duration * (Math.PI / 2))) + initial;
        }

        public static float OutSine(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Sin(time / duration * (Math.PI / 2)) + initial;
        }

        public static float InOutSine(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta / 2 * (1 - (float)Math.Cos(Math.PI * time / duration)) + initial;
        }

        #endregion

        #region expo

        public static float InExpo(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Pow(2, 10 * (time / duration - 1)) + initial;
        }

        public static float OutExpo(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)((-1) * Math.Pow(2, -10 * time / duration) + 1) + initial;
        }

        public static float InOutExpo(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return delta / 2 * (float)Math.Pow(2, 10 * (time - 1)) + initial;
            return delta / 2 * (float)(-1 * Math.Pow(2, -10 * --time) + 2) + initial;
        }

        #endregion

        #region circ

        public static float InCirc(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return -delta * (float)(Math.Sqrt(1 - (float)Math.Pow(time / duration, 2)) - 1) + initial;
        }

        public static float OutCirc(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta * (float)Math.Sqrt(1 - (float)Math.Pow(time / duration - 1, 2)) + initial;
        }

        public static float InOutCirc(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if ((time /= duration / 2) < 1) return -delta / 2 * (1 - (float)Math.Sqrt(1 - (float)Math.Pow(time, 2))) + initial;
            return delta / 2 * (float)(Math.Sqrt(1 - Math.Pow(time - 2, 2)) + 1) + initial;
        }

        #endregion

        #region elastic

        public static float InElastic(float time, float duration, float initial, float final, float amplitude, float period) {
            float delta = final - initial;

            if (time == 0) return initial;
            if ((time /= duration) == 1) return initial + delta;
            if (period == 0) period = duration * .3f;

            double s = 0;
            if (amplitude == 0 || amplitude < Math.Abs(delta)) {
                amplitude = delta;
                s = period / 4;
            } else {
                s = period / (2 * Math.PI) * Math.Asin(delta / amplitude);
            }

            double mathPow = Math.Pow(2, 10 * (time -= 1));
            double mathSin = Math.Sin((time * duration - s) * (2 * Math.PI) / period);

            return (float)(-(amplitude * mathPow * mathSin) + initial);
        }

        public static float OutElastic(float time, float duration, float initial, float final, float amplitude, float period) {
            float delta = final - initial;

            if (time == 0) return initial;
            if ((time /= duration) == 1) return initial + delta;
            if (period == 0) period = duration * .3f;

            double s = 0;
            if (amplitude == 0 || amplitude < Math.Abs(delta)) {
                amplitude = delta;
                s = period / 4;
            } else {
                s = period / (2 * Math.PI) * Math.Asin(delta / amplitude);
            }

            double mathPow = Math.Pow(2, -10 * time);
            double mathSin = Math.Sin((time * duration - s) * (2 * Math.PI) / period);

            return (float)((amplitude * mathPow * mathSin) + delta + initial);
        }

        public static float InOutElastic(float time, float duration, float initial, float final, float amplitude, float period) {
            float delta = final - initial;

            if (time == 0) return initial;
            if ((time /= duration / 2) == 2) return initial + delta;
            if (period == 0) period = duration * .3f * 1.5f;

            double s = 0;
            if (amplitude == 0 || amplitude < Math.Abs(delta)) {
                amplitude = delta;
                s = period / 4;
            } else {
                s = period / (2 * Math.PI) * Math.Asin(delta / amplitude);
            }


            if (time < 1) {
                double mathPow = Math.Pow(2, 10 * (time -= 1));
                double mathSin = Math.Sin((time * duration - s) * (2 * Math.PI) / period);
                return (float)(-.5f * (amplitude * mathPow * mathSin) + initial);
            } else {
                double mathPow = Math.Pow(2, -10 * (time -= 1));
                double mathSin = Math.Sin((time * duration - s) * (2 * Math.PI) / period);
                return (float)(amplitude * mathPow * mathSin * .5f + delta + initial);
            }
        }

        #endregion

        #region back

        public static float InBack(float time, float duration, float initial, float final, float overshoot) {
            float delta = final - initial;
            float s = overshoot;
            if (s == 0)
                s = 1.70158f;

            return delta * (time /= duration) * time * ((s + 1) * time - s) + initial;
        }

        public static float OutBack(float time, float duration, float initial, float final, float overshoot) {
            float delta = final - initial;
            float s = overshoot;
            if (s == 0)
                s = 1.70158f;

            return delta * ((time = time / duration - 1) * time * ((s + 1) * time + s) + 1) + initial;
        }

        public static float InOutBack(float time, float duration, float initial, float final, float overshoot) {
            float delta = final - initial;
            float s = overshoot;
            if (s == 0)
                s = 1.70158f;

            if ((time /= duration / 2) < 1) return delta / 2 * (time * time * (((s *= (1.525f)) + 1) * time - s)) + initial;
            return delta / 2 * ((time -= 2) * time * (((s *= (1.525f)) + 1) * time + s) + 2) + initial;
        }

        #endregion

        #region bounce

        public static float InBounce(float time, float duration, float initial, float final) {
            float delta = final - initial;
            return delta - MyEase.OutBounce((float)(duration - time), duration, 0, final - initial) + initial;
        }

        public static float OutBounce(float time, float duration, float initial, float final) {
            float delta = final - initial;
            time /= duration;
            if (time < (1 / 2.75f)) return (float)(delta * (7.5625 * time * time) + initial);
            if (time < (2 / 2.75f)) return (float)(delta * (7.5625 * (time -= 1.5f / 2.75f) * time + .75f) + initial);
            if (time < (2.5f / 2.75f)) return (float)(delta * (7.5625 * (time -= 2.25f / 2.75f) * time + .9375f) + initial);

            return (float)(delta * (7.5625 * (time -= 2.625f / 2.75f) * time + .984375f) + initial);
        }

        public static float InOutBounce(float time, float duration, float initial, float final) {
            float delta = final - initial;
            if (time < duration / 2) return MyEase.InBounce((float)(2 * time), duration, 0, final - initial) * .5f + initial;
            return MyEase.OutBounce((float)(time * 2 - duration),duration, 0, final - initial) * .5f + delta * .5f + initial;
        }

        #endregion

        #endregion

    }
}
