function Y = classify(Model,xTest)
  xTest = extractFea(xTest);
  
  % Y = nbClassify(Model,xTestFea);
  
  % [ Y ] = sfmClassify( Model, xTest' ) - 1;
  
  [ Y ] = nnClassify( Model, xTest )-1;
  end
