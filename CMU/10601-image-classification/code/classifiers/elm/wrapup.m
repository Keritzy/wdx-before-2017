% train different Model
Model = elmTrain(feas, trainY, 6400, 'sig');
Model1 = elmTrain(feas, trainY, 6400, 'sin');
Model2 = elmTrain(feas, trainY, 6400, 'radbas');

% save
save('Model.mat', 'Model');
save('Model1.mat', 'Model1');
save('Model2.mat', 'Model2');

