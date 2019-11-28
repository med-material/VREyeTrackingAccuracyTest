# VR Eye Tracking Accuracy Test
#### VR Eye Tracking Accuracy Test is a Unity App to measure eye tracking accuracy in a VR headset.
![VR Eye Tracking Accuracy Test Screen](https://raw.githubusercontent.com/med-material/VREyeTrackingAccuracyTest/master/accuracy-test-image.png)

## About
VR accuracy test is used to determine accuracy of eye tracking devices. It includes features...
* Integration for Pupil Labs Eye Trackers.
* User interface for person in non-VR (facilitator), to control the test.
* Shows results of the test, after the test has finished.
* Ability to upload data to database and visualize with R Shiny.

VR Eye Tracking Accuracy Test was made with Unity and use C# scripts.   
VR Tool to Test the Accuracy of the Pupil Labs Eye Tracking Devices mounted in a VR Headset (Tested for HTC Vive).

## Why use this project?
This test can be run before use eye tracking into an other project and know exactly if eye tracking work good or not. So after you run this test, you can know if the patient sees correctly or if you have any imprecision with the eye tracking.

## Who can use this project?
Anyone want to know the accuracy from his vr eye tracking stuff, can use this project to know it before run an other program.   
_It can be a doctor to test the view of a patient, and detect any potential disease._

 -----------------  
 
### Pupil Lab plugin
![Pupil plugin picture](https://raw.githubusercontent.com/wiki/pupil-labs/pupil/media/images/pupil_labs_pupil_core_repo_banner.jpg)   
Integrate an open source eye tracking platform, with an active community to always maintain new features and improve the system.   
Find the pupil plugin version used in this project here: [Pupil plugin installed](https://github.com/med-material/hmd-eyes)

### UI to control the test
User interface for person in non-VR (facilitator), to control the test is implemented. So the facilitator can follow all the test in real time, show the results at the end and configure it.   
![UI configuration screen](https://i.imgur.com/NJ1rk83.png)

### Show results immediately
At the end of test, you cah show directly in the app, the results of the participant. We can show the eye tracking stuff accuracy and the gaze of participant.   
![UI results screen](https://i.imgur.com/UE6HduX.png)

### RShiny App
A RShiny App was made in parallel of this project to visualize the data collected in the database. The current App show you the eye tracking accuracy of a selected participant or all participant. You can also choose the target.      
![RShiny App UI](https://github.com/YoNeXia/VREyeTrackingAccuracyShinyApp/blob/master/shiny_app_screen.png?raw=true)
Find the RShiny App in this repository: [VREyeTrackingAccuracyShinyApp](https://github.com/YoNeXia/VREyeTrackingAccuracyShinyApp)

## Contributors
Done at Aalborg University.   
- **Bastian ILSO** - _Owner_ - [MED Material](https://github.com/med-material)
- **Yohann NERAUD** - _Developer_ - [Git](https://gitlab.com/YoNeXia)
- **Romain JUNCA** - _Developer_ - [Git](https://github.com/RomainJunca)
- **Arthur SALVAT** - _Developer_ - [Git](https://github.com/Shiks67)
- **Tanguy BLOCHET** - _Developer_ - [Git](https://github.com/TngBlt)
- **Alexandre MONNIER** - _Developer_

 -----------------  
# Technical details
## Getting started
To use this project on your computer, just check releases and find the App to run it or UnityPackage to add it into your Unity project.   
_[Releases VREyeTrackingAccuracyTest page](https://github.com/med-material/VREyeTrackingAccuracyTest/releases)_

## Eye Tracker Data Collection in the Accuracy Test
With this package you’re going to have logs saved in your “My Documents” file, “_GazeData” and then the file at the current date (e.g. 01-29-19) and finally the .csv file with the time where you started the application + the id (e.g. 02-43-00_id-778899.csv).   
Logs the accuracy for each circle and how much time the participant had to diminish it.   
 * **User ID**: user's ID letters / numbers included, some special char that can't be saved as filename won't be saved (e.g. User123)
 * **Date - Time**: date and time you started the application (e.g. 01/29/19 - 03:01:17)
 * **Wearing make-up**: Is the user wearing make-up (e.g. Yes or No)
 * **Wearing glasses**: Is the user wearing glasses (e.g. Yes or No)
 * **Gaze dot displayed**: Is the gaze dot displayed or not (e.g. Yes or No)
 * **Grid displayed**: Is the grid displayed or not in the accuracy test, the grid where the target (circles) appear (e.g. Yes or No)
 * **Using input**: Is the user using input (spacebar) to switch targets in accuracy test scene (e.g. Yes or No)
 * **Target lifespan (ms)**: Lifespan of the targets if the input mode is off (e.g. 4500)
 * **Session Time**: sessions timer, application running since, in seconds (e.g. 58.432)
 * **Scene Name**: current Unity scene's name
 * **Event**: current event running (e.g. Configuration is running, Calibration is running, Accuracy Test is counting down, Accuracy Test is running, Accuracy Test ended)
 * **Scene Timer**: current Unity scene's timer, Unity scene running since, in seconds (e.g. 58.432)
 * **framerate**: current framerate fps (e.g. 60)
 * **circle_pos_x**: target's x position on the accuracy test on the grid (-30 left 0 middle 30 right)
 * **circle_pos_y**: target's y position on the accuracy test on the grid (-30 down 0 middle 30 up)
 * **pupilData_gaze_x**: viewport x position
 * **pupilData_gaze_y**: viewport y position
 * **gaze_on_grid_x**: gaze point x position on the grid on the accuracy test scene (e.g. 28)
 * **gaze_on_grid_y**: gaze point y position on the grid on the accuracy test scene (e.g. 16)
 * **Left_eye_conf**: left eye confidence (0 to 1 => 0 to 100%)
 * **Right_eye_conf**: right eye confidence (0 to 1 => 0 to 100%)
 * **gaze_confidence**: both eyes confidence (0 to 1 => 0 to 100%)
 * **circle_radius**: targets current radius on the accuracy test scene (e.g. 8.6)
 * **Offset X**: current offset on X axis from the center of the current target in circle test scene (e.g. 45)
 * **Offset Y**: current offset on Y axis from the center of the current target in circle test scene (e.g. 38)
 * **Time_To_First_Fix**: time to first fix on the current target, NaN until we look at the current target and stay at the same time in seconds (e.g. 4.889)
