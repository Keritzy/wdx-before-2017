fid = fopen('Feartures.txt','w');
tic;
for t=1:50000
    Red=trainX(t,1:1024); 
    Green=trainX(t,1025:2048); 
    Blue=trainX(t,2049:3072);

 	Image(:,:,1)=reshape(Red,32,32); 
    Image(:,:,2)=reshape(Green,32,32);
 	Image(:,:,3)=reshape(Blue,32,32);

clear param;
%param.imageSize = [256 256]; % it works also with non-square images
param.orientationsPerScale = [8 8 8 8];
param.numberBlocks = 4;
param.fc_prefilt = 4;

[gist1, param] = LMgist(Image, '', param);
fprintf(fid,'%d ',trainX(t));
for ii=1:512   
  %fprintf(fid,'%d:%g %d:%g %d:%g %d:%g %d:%g %d:%g %d:%g %d:%g ',ii,gist1(1,ii),ii+2,gist1(1,ii+2),ii+3,gist1(1,ii+3),ii+4,gist1(1,ii+4),ii+5,gist1(1,ii+5),ii+6,gist1(1,ii+6),ii+7,gist1(1,ii+7));
    fprintf(fid,'%d:%g ', ii, gist1(1, ii));
end
 if (mod(t,100)==0)
     t
 end
    fprintf(fid,'\n');
% Visualization
end
fclose(fid);
fprintf('consuming time %f minutes \n ',toc/60.0);
