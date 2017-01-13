% http://www.vlfeat.org/overview/sift.html
clc ; clear;
I = vl_impattern('roofs1') ;
image(I) ;
% The vl_sift command requires a single precision gray scale image. 
% It also expects the range to be normalized in the [0,255] interval
% (while this is not strictly required, the default values of some internal
% thresholds are tuned for this case). The image I is converted in the appropriate format by
I = single(rgb2gray(I)) ;
[f,d] = vl_sift(I) ;
% The matrix f has a column for each frame. A frame is a disk of center f(1:2), 
% scale f(3) and orientation f(4) . We visualize a random selection of 50 features by:
perm = randperm(size(f,2)) ;
sel = perm(1:50) ;
h1 = vl_plotframe(f(:,sel)) ;
h2 = vl_plotframe(f(:,sel)) ;
set(h1,'color','k','linewidth',3) ;
set(h2,'color','y','linewidth',2) ;
% We can also overlay the descriptors by
h3 = vl_plotsiftdescriptor(d(:,sel),f(:,sel)) ;
set(h3,'color','g') ;