namespace common.util {
    public static class TextUtil {
        public static string TimeToStr(long sec) {
            if (sec <= 0) return "0s";
            var text = "";
            var mc = sec / 2592000;
            if (mc > 0) {
                sec -= mc * 2592000;
                text += mc + "msc ";
            }

            var d = sec / 86400;
            if (d > 0) {
                sec -= d * 86400;
                text += d + "d ";
            }

            var h = sec / 3600;
            if (h > 0) {
                sec -= h * 3600;
                text += h + "h ";
            }

            var m = sec / 60;
            if (m > 0) {
                sec -= m * 60;
                text += m + "min ";
            }

            return sec > 0 ? text + sec + "s" : text;
        }
    }
}