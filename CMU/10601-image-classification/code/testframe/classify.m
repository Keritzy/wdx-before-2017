function [Y,err] = classify(Model,xTest,yTest)
  xTestFea = extractFea(xTest);
  
  % Y = nbClassify(Model,xTestFea);
  
  % [ Y ] = sfmClassify( Model, xTestFea' ) - 1;
  
  [ Y ] = nnClassify( Model, xTestFea )-1;
  
  
  %% format
  
  Y = uint8(Y);
  
  %% err
  err = 0;
  if exist('yTest','var')
    err = sum(abs(Y - yTest) > 0) / size(Y,1);
  end
  end
