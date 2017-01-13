feas = [];
for i = 1 : size(trainX,1)
    feas = [feas;naivehog(reshape(trainX(i,:),[32,32,3]))'];
    i
end
save('hogfea.mat','feas')
