# Add project specific ProGuard rules here.
# You can control the set of applied configuration files using the
# proguardFiles setting in build.gradle.
#
# For more details, see
#   http://developer.android.com/guide/developing/tools/proguard.html

# If your project uses WebView with JS, uncomment the following
# and specify the fully qualified class name to the JavaScript interface
# class:
#-keepclassmembers class fqcn.of.javascript.interface.for.webview {
#   public *;
#}

# Uncomment this to preserve the line number information for
# debugging stack traces.
#-keepattributes SourceFile,LineNumberTable

# If you keep the line number information, uncomment this to
# hide the original source file name.
#-renamesourcefileattribute SourceFile
-allowaccessmodification
-optimizations !code/simplification/arithmetic
-keepattributes *Annotation*
-dontskipnonpubliclibraryclasses
-optimizationpasses 5
-printmapping map.txt
-flattenpackagehierarchy

#-libraryjars  libs/glide-4.11.0.jar
#-libraryjars  libs/okhttp-4.9.0.jar
#-libraryjars  libs/gson-2.8.6.jar

-keep public class org.hse.android.MainActivity
-keep public class org.hse.android.BaseActivity
-keep public class org.hse.android.ScheduleActivity
-keep public class org.hse.android.TeacherActivity
-keep public class org.hse.android.StudentActivity
-keep public class org.hse.android.SettingsActivity

-keep public class * extends android.app.Activity

-keep public class * extends android.view.View {
public <init>(android.content.Context);
public <init>(android.content.Context, android.util.AttributeSet);
public <init>(android.content.Context, android.util.AttributeSet, int);
public void set*();
}
