# VR Eye Tracking Accuracy Test
![Virtual Reality Baguette Cutting Task](https://raw.githubusercontent.com/med-material/VREyeTrackingAccuracyTest/master/accuracy-test-image.png)

VR Tool to Test the Accuracy of the Pupil Labs Eye Tracking Devices mounted in a VR Headset (Tested for HTC Vive)
 
Unity (C# script)

Logs the accuracy for each circle and how much time the participant had to diminish it.

Done at Aalborg University, internship (24/09/18 to 01/02/19)

## Eye Tracker Data Collection in the Accuracy Test
With this package you’re going to have logs saved in your “My Documents” file, “_GazeData” and
then the file at the current date (e.g. 01-29-19) and finally the .csv file with the time where you
started the application + the id (e.g. 02-43-00_id-778899.csv).
 * **User ID**: user's ID letters / numbers included, some special char that can't be saved as filename won't be saved (e.g. User123)
 * **Date - Time**: date and time you started the application (e.g. 01/29/19 - 03:01:17)
 * **Wearing make-up**: Is the user wearing make-up (e.g. Yes or No)
 * **Wearing glasses**: Is the user wearing glasses (e.g. Yes or No)
 * **Gaze dot displayed**: Is the gaze dot displayed or not (e.g. Yes or No)
 * **Grid displayed**: Is the grid displayed or not in the accuracy test, the grid where the target (circles) appear (e.g. Yes or No)
 * **Using input**: Is the user using input (spacebar) to switch targets in accuracy test scene (e.g. Yes or No)
 * **Target lifespan (ms)**: Lifespan of the targets if the input mode is off (e.g. 4500)
 * **Session Time**: sessions timer, application running since, in seconds (e.g. 58.432)
 * **Scene Name**: current Unity scene's name (e.g. Field of view or CircleTest)
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


Contributors :
Alexandre MONNIER, Tanguy BLOCHET, Romain JUNCA, Arthur SALVAT, Bastian Ilso, Yohann NERAUD.
