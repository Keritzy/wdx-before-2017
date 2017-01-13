

[ imgs,labels,label_names ] = loadImages(false);
% [X,Y,C,N] = size(imgs);

[X,Y,N] = size(imgs);
K=200;
visWord = siftBuild(imgs,K);
save('meta/siftVisWord.mat','visWord');



%% extract feature from all images
load('meta/siftVisWord.mat');

[ bow ] = extractBowSiftFea(imgs,visWord );

bow = zeros(K,N);
for i = 1 : N
bow(:,i) = bowVisWord( imgs(:,:,i), visWord );
end

save('meta/bowSiftFea.mat','bow');

%% test nbTrain

train_set = getSubset( bow,1000,5000,10,1 );
train_lab = getSubset(labels',1000,5000,10,1);
test_set = getSubset( bow,4000,5000,10,1+1000 );
test_lab = getSubset(labels',4000,5000,10,1+1000);

mdl = nbTrain(train_set',train_lab);
res = nbClassify(mdl,test_set');

nb = NaiveBayes.fit(train_set, train_lab);
Y = predict(nb,test_set);
cMat = confusionmat(test_lab, train_lab);
siftAcc = sum(diag(cMat))/sum(sum(cMat));