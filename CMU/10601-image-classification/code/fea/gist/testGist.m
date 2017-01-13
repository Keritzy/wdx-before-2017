loaddata;

fprintf('Creating gistFeatures file...\n');
fid = fopen('gistFeartures.txt','w');
fprintf('Creating gistFeatures file done\n');

fprintf('Start Extracting feature...patient\n');
tic;

% number of pictures
N = 50;

for t=1:N % test with 500 first
    fprintf('Extract gist feature for Image %d / %d...\n',t, N);
    
    clear param;
    param.orientationsPerScale = [8 8 8 8];
    param.numberBlocks = 4;
    param.fc_prefilt = 4;

    [gist1, param] = LMgist(imgs(:,:,:,t), '', param);
    fprintf(fid,'%d ',trainY(t));
    for ii=1:512 
        %fprintf(fid,'%d:%g %d:%g %d:%g %d:%g %d:%g %d:%g %d:%g %d:%g ',ii,gist1(1,ii),ii+2,gist1(1,ii+2),ii+3,gist1(1,ii+3),ii+4,gist1(1,ii+4),ii+5,gist1(1,ii+5),ii+6,gist1(1,ii+6),ii+7,gist1(1,ii+7));
        fprintf(fid,'%d:%g ', ii, gist1(1, ii));
    end
    if (mod(t,100)==0)
        t;
    end
    fprintf(fid,'\n');
% Visualization
end
fclose(fid);
fprintf('Training time %f seconds \n ',toc);
