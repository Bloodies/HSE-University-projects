package org.hse.android;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.FileProvider;
import android.Manifest;
import android.annotation.SuppressLint;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.Objects;

import com.bumptech.glide.Glide;

public class SettingsActivity extends AppCompatActivity implements SensorEventListener {

    private final static String TAG = "SettingsActivity";

    private static final int REQUEST_IMAGE_CAPTURE = 10;
    private static final int REQUEST_PERMISSION_CODE = 1;

    private final static String PERMISSION = Manifest.permission.CAMERA;

    private ImageView photo;
    private String imageFilePath;
    private EditText name;

    private PreferenceManager preferenceManager;
    private SensorManager sensorManager;
    private Sensor light;
    private TextView sensorLight;

    Uri photoURI;
    File imagePath;
    File image;

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        Objects.requireNonNull(getSupportActionBar()).hide();

        preferenceManager = new PreferenceManager(this);
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        light = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);

        View buttonPhoto = findViewById(R.id.button_photo);
        buttonPhoto.setOnClickListener(v -> checkPermission());

        View buttonSave = findViewById(R.id.button_save);
        buttonSave.setOnClickListener(v -> {
            loadPhoto();
            preferenceManager.savePhotoValue("photo", image.getPath());
            Toast.makeText(SettingsActivity.this, "Фото сохранено", Toast.LENGTH_SHORT).show();
        });

        name = findViewById(R.id.name);
        photo = findViewById(R.id.photo);
        sensorLight = findViewById(R.id.sensor_light);

        File imgFile = new File(preferenceManager.getPhotoValue("photo"));
        if(imgFile.exists()){ Glide.with(this).load(imgFile).into(photo); }
        else{ photo.setImageResource(R.drawable.no_image); }
        loadPhoto();

        if(sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT) == null) {
            sensorLight.setText("Нет датчика освещенности");
        }
    }

    @Override
    protected void onResume() {
        super.onResume();

        File imgFile = new File(preferenceManager.getPhotoValue("photo"));
        if (imgFile.exists()) { Glide.with(this).load(imgFile).into(photo); }
        else { photo.setImageResource(R.drawable.no_image); }

        sensorManager.registerListener(this, light, SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onPause() {
        super.onPause();

        File imgFile = new File(preferenceManager.getPhotoValue("photo"));
        if(imgFile.exists()){ Glide.with(this).load(imgFile).into(photo); }
        else{ photo.setImageResource(R.drawable.no_image); }

        sensorManager.unregisterListener(this);
    }

    public final void onAccuracyChanged(Sensor sensor, int accuracy) { }

    @SuppressLint("SetTextI18n")
    public final void onSensorChanged(SensorEvent event) {
        float lux = event.values[0];
        sensorLight.setText("{lux} lux");
    }

    public void checkPermission(){
        int permissionsCheck = ActivityCompat.checkSelfPermission(this, PERMISSION);
        if (permissionsCheck != PackageManager.PERMISSION_GRANTED) {
            if(ActivityCompat.shouldShowRequestPermissionRationale(this, PERMISSION)) {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.setTitle("Нужно предоставить права")
                        .setMessage("Для снятия фото нужно предоставить права на фото")
                        .setPositiveButton(android.R.string.ok, (dialog, id) ->
                                requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE));
                builder.create().show();
            }
            else { requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE); }
        }
        else { dispatchTakePictureIntent(); }
    }
    
    private void requestPermissions(String permissionName, int permissionRequestCode) {
        ActivityCompat.requestPermissions(this, new String[]{permissionName}, permissionRequestCode);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] result) {
        if(requestCode == REQUEST_PERMISSION_CODE) {
            if(result.length > 0 && result[0] == PackageManager.PERMISSION_GRANTED) {
                dispatchTakePictureIntent();
            }
            else {
                Log.d(TAG, "Permission not granted");
                requestPermissions(PERMISSION, REQUEST_PERMISSION_CODE);
            }
        }
    }

    @SuppressLint("QueryPermissionsNeeded")
    private void dispatchPictureIntent(){
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        if (takePictureIntent.resolveActivity(getPackageManager()) != null){
            File photoFile = null;
            try{ photoFile = createImageFile(); }
            catch(Exception ex){ Log.e("tag", "Create file: ", ex); }
            if(photoFile != null){
                photoURI = FileProvider.getUriForFile(this, BuildConfig.APPLICATION_ID + ".provider", photoFile);
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI);
                try{ startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE); }
                catch(ActivityNotFoundException ex){ Log.e("tag", "Start activity: ", ex); }
            }
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode == RESULT_OK){
            loadPhoto();
            return;
        }
        super.onActivityResult(requestCode, resultCode, data);
    }

    private void loadPhoto() {
        if(image != null) { Glide.with(this).load(image).into(photo); }
    }

    @SuppressLint("QueryPermissionsNeeded")
    private void dispatchTakePictureIntent() {
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        if(takePictureIntent.resolveActivity(getPackageManager()) != null) {
            //Create a file to store the image
            File photoFile = null;
            try { photoFile = createImageFile(); }
            catch (IOException ex) { Log.e(TAG, "Create file", ex); }
            if(photoFile != null) {
                Uri photoURI = FileProvider.getUriForFile(this, BuildConfig.APPLICATION_ID + ".provider", photoFile);
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT,
                        photoURI);
                try { startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE); }
                catch (ActivityNotFoundException e) { Log.e(TAG, "Start activity", e); }
            }
        }
        loadPhoto();
    }

    private File createImageFile() throws IOException {
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.getDefault()).format(new Date());
        String imageFileName = "IMG_" + timeStamp + "_";

        imagePath = new File(getFilesDir(), "external_files");
        imagePath.mkdir();
        image = new File(imagePath.getPath(), String.format("%s.img", imageFileName));
        return image;
    }
}