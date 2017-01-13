function labels = classify(Model, X)

tfeas = [];
for i = 1 : size(X,1)
    tfeas = [tfeas;naivehog(reshape(X(i,:),[32,32,3]))'];
end

testX = tfeas';


testDataCount = size(testX,2);

tempH = double(Model.inputWeight)*testX;
clear testX;

ind = ones(1, testDataCount);
biasMat = Model.hiddenNeuronBias(:,ind);
tempH = tempH + biasMat;

switch Model.kernel
    case {'sig'}
        H = 1./ (1 + exp(-tempH));
    case {'sin'}
        H = sin(tempH);
    case {'hardlim'}
        H = hardlim(tempH);
    case {'tribas'}
        H = tribas(tempH);
    case {'radbas'}
        H = radbas(tempH);
end

predict = (H' * Model.outputWeight)';
[~, labels] = max(predict);
labels = labels' - 1;

end

% fprintf('Test accuracy %f%%\n', 100 * (1 - sum(labels ~= testY) / length(testY)));

