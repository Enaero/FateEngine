# Common utilities between Fate and Godot

IMPORTANT: Keep this minimal! The idea is to keep the 
fate engine portable between game engines. Things defined
here in Common can depend on Godot specific libraries, which means
they will need to be ported if we ever switch engines.

Expect a full re-write/refactor of everything in the Common namespace
if we swap engines. So keep it as small and light as possible.