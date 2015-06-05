# Random Alarm Clock / Timer
A simple app that randomly alerts you via a system tray balloon tip. It can be used to remind you of things like taking a quick stand-up break, drink some water, check your posture, etc... The goal is to not have a fixed amount of time but a random amount unpredictable amound of time.  If the time were fixed a brain would quickly figure out this patern where as if the time is always changing it cannot be prodicted.  If you were going to ketch someone doing something would you check on them with a fixed intervol or a random?  Probably random. 

##Notes
* It can have up to four different notifications and the average random time for each can be adjusted. 
* A peak ahead can be enabled.
* The Random Timer class is in its own class so that it can be used in other projects. It's pretty much a drop in class that works much like the System.Timers.Timer class. It actually uses System.Timers.Timer also.
