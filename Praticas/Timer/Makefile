# MPLAB IDE generated this makefile for use with GNU make.
# Project: Timer.mcp
# Date: Wed Apr 08 21:20:46 2026

AS = MPASMWIN.exe
CC = mcc18.exe
LD = mplink.exe
AR = mplib.exe
RM = rm

Timer.cof : main.o intimer.o
	$(LD) /p18F4550 /l"C:\Program Files (x86)\Microchip\mplabc18\v3.47\lib" "main.o" "intimer.o" /u_CRUNTIME /u_DEBUG /z__MPLAB_BUILD=1 /z__MPLAB_DEBUG=1 /o"Timer.cof" /M"Timer.map" /W

main.o : main.c ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/timers.h main.c ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/p18f4550.h ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/pconfig.h ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/p18cxxx.h intimer.h
	$(CC) -p=18F4550 "main.c" -fo="main.o" -D__DEBUG -Ou- -Ot- -Ob- -Op- -Or- -Od- -Opa-

intimer.o : intimer.c ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/timers.h intimer.c intimer.h ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/p18f4550.h ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/pconfig.h ../../../../../../../../Program\ Files\ (x86)/Microchip/mplabc18/v3.47/h/p18cxxx.h
	$(CC) -p=18F4550 "intimer.c" -fo="intimer.o" -D__DEBUG -Ou- -Ot- -Ob- -Op- -Or- -Od- -Opa-

clean : 
	$(RM) "main.o" "intimer.o" "Timer.cof" "Timer.hex"

