function Y = classify(Model,xTest)
  xTestFea = extractFea(xTest);
  
  Y = nbClassify(Model,xTestFea);
  
  % [ Y ] = sfmClassify( Model, xTestFea' );
  
  % [ Y ] = nnClassify( Model, xTestFea );
  end
