
fs = require('fs')
_u = require('underscore')
lines = _u.rest fs.readFileSync('../../tests/A-large-practice.in').toString().split('\n')

C = 1
boardRow = 0
xB = {}; yB = {} 

won = (board) ->
  for p,v of board
    return true if v == 4
  false

updateBoard = (board, r, c) ->
  board["R#{r}"]++
  board["C#{c}"]++
  board["D"]++  if c == r 
  board["AD"]++ if c == 3-r

readMove = (move, boardCol, l) ->
  if move is '.' then xB.ended = false
  
  (updateBoard xB, boardRow, boardCol) if move is 'X' or move is 'T'
  (updateBoard yB, boardRow, boardCol) if move is 'O' or move is 'T'
  #(update(yB boardRow boardCol)) if move is 'O' or move is 'T'

lines.forEach((line) ->
  line = line.trim()
  if line is ''
    #console.log 'Skip empty line?', line
    return

  if boardRow is 0
    xB = ended: true, R0: 0, R1: 0, R2: 0, R3: 0, C0: 0, C1: 0, C2: 0, C3: 0, D: 0, AD: 0
    yB = ended: true, R0: 0, R1: 0, R2: 0, R3: 0, C0: 0, C1: 0, C2: 0, C3: 0, D: 0, AD: 0
  
  boardCol = 0
  _u.each line, readMove  
  boardRow++
  
  if boardRow is 4
    process.stdout.write "Case ##{C}: "
    if won xB then console.log 'X won'
    else if won yB then console.log 'O won'
    else if xB.ended then console.log 'Draw'
    else console.log 'Game has not completed'
    
    boardRow = 0
    #console.log C, xB, yB
    C++

)

#console.log _u.isArray lines
#console.log yB

#xB = ended: true, R0: 0, R1: 0, R2: 0, R3: 0, C0: 0, C1: 0, C2: 0, C3: 0, D: 0, AD: 0
#console.log xB
#updateBoard xB, 0, 2
#updateBoard xB, 1, 1
#updateBoard xB, 2, 1
#console.log xB